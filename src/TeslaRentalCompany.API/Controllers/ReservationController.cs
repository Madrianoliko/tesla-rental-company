using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TeslaRentalCompany.API.Services;
using TeslaRentalCompany.API.Entities;
using TeslaRentalCompany.API.Models;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/car/{carId}/reservation")]
    [Authorize(Policy = "MustBeAdmin")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        public ITeslaRentalCompanyRepository Repository { get; }
        public IMapper Mapper { get; }

        public ReservationController(
            ITeslaRentalCompanyRepository repository,
            IMapper mapper)
        {
            Repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            Mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservationsForCarAsync(int carId)
        {
            if (!await Repository.CarExistsAsync(carId))
            {
                return NotFound();
            }

            var reservationsForCar = await Repository.GetReservationsForCarAsync(carId);

            return Ok(Mapper.Map<IEnumerable<ReservationDto>>(reservationsForCar));
        }
        [HttpGet("{reservationId}", Name = "GetReservationForCar")]
        public async Task<IActionResult> GetReservationForCarAsync(int carId, int reservationId)
        {
            if (!await Repository.CarExistsAsync(carId))
            {
                return NotFound();
            }

            var reservationForCar = await Repository.GetReservationForCarAsync(carId, reservationId);

            if (reservationForCar == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<ReservationDto>(reservationForCar));
        }
        [HttpPost]
        public async Task<ActionResult<ReservationDto>> CreateReservation(int carId,
            ReservationForCreationDto reservation)
        {
            if (!await Repository.CarExistsAsync(carId))
            {
                return NotFound();
            }

            var finalReservation = Mapper.Map<Reservation>(reservation);

            await Repository.AddReservationForCarAsync(carId, finalReservation);

            await Repository.SaveChangesAsync();

            var createdReservationToReturn =
                Mapper.Map<ReservationDto>(finalReservation);

            return CreatedAtRoute("GetReservationForCar",
                new
                {
                    carId = carId,
                    reservationId = createdReservationToReturn.Id
                },
                createdReservationToReturn);
        }
        [HttpPut("{reservationId}")]
        public async Task<ActionResult> UpdateReservation(
            int carId,
            int reservationId,
            ReservationForUpdatingDto reservation)
        {
            if (!await Repository.CarExistsAsync(carId))
            {
                return NotFound();
            }

            var reservationEntity = await Repository.GetReservationForCarAsync(carId, reservationId);

            if (reservationEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(reservation, reservationEntity);

            await Repository.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("reservationId")]
        public async Task<ActionResult> DeleteReservation(int carId, int reservationId)
        {
            if (!await Repository.CarExistsAsync(carId))
            {
                return NotFound();
            }

            var reservationEntity = await Repository.GetReservationForCarAsync(carId, reservationId);

            if (reservationEntity == null)
            {
                return NotFound();
            }

            Repository.DeleteReservationForCar(reservationEntity);

            await Repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
