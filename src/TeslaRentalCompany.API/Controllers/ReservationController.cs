using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TeslaRentalCompany.API.Services;
using TeslaRentalCompany.API.Entities;
using TeslaRentalCompany.API.Models;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/reservation")]
    
    [ApiController]
    public class ReservationController : ControllerBase
    {

        public ReservationController(
            ITeslaRentalCompanyRepository repository,
            IMapper mapper)
        {
            Repository = repository ??
                throw new ArgumentNullException(nameof(repository));
            Mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public ITeslaRentalCompanyRepository Repository { get; }
        public IMapper Mapper { get; }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservationsAsync()
        {
            var reservationEntities = await Repository.GetReservationsAsync();

            return Ok(Mapper.Map<IEnumerable<ReservationDto>>(reservationEntities));
        }

        [Authorize]
        [HttpGet("{reservationId}", Name = "GetReservation")]
        public async Task<IActionResult> GetReservationAsync(int reservationId)
        {
            var reservationForCar = await Repository.GetReservationAsync(reservationId);

            if (reservationForCar == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<ReservationDto>(reservationForCar));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ReservationDto>> CreateReservation(int carId, int userId,
            ReservationForCreationDto reservation)
        {
            if (!await Repository.CarExistsAsync(carId))
            {
                return NotFound();
            }
            if (!await Repository.UserExistsAsync(userId))
            {
                return NotFound();
            }

            var finalReservation = Mapper.Map<Reservation>(reservation);

            await Repository.AddReservationAsync(carId, userId, finalReservation);

            await Repository.SaveChangesAsync();

            var createdReservationToReturn =
                Mapper.Map<ReservationDto>(finalReservation);

            return CreatedAtRoute("GetReservation",
                new
                {
                    carId = carId,
                    userId = userId,
                    reservationId = createdReservationToReturn.Id
                },
                createdReservationToReturn);
        }
        [Authorize]
        [HttpPut("{reservationId}")]
        public async Task<ActionResult> UpdateReservation(
            int reservationId,
            ReservationForUpdatingDto reservation)
        {
            var reservationEntity = await Repository.GetReservationAsync(reservationId);

            if (reservationEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(reservation, reservationEntity);

            await Repository.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Policy = "MustBeAdmin")]
        [HttpDelete("{reservationId}")]
        public async Task<ActionResult> DeleteReservation(int reservationId)
        {
            var reservationEntity = await Repository.GetReservationAsync(reservationId);

            if (reservationEntity == null)
            {
                return NotFound();
            }

            Repository.DeleteReservation(reservationEntity);

            await Repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
