using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TeslaRentalCompany.API.Interfaces;
using TeslaRentalCompany.Data;
using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IMailService _mailService;
        private readonly ITeslaRentalCompanyRepository _repository;
        private readonly IMapper _mapper;

        public ReservationController(
            ILogger<ReservationController> logger,
            IMailService mailService,
            ITeslaRentalCompanyRepository repository,
            IMapper mapper
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //seedData = seedData ?? throw new ArgumentNullException(nameof(seedData));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReservationDto>> GetReservations()
        {
            var reservations = seedData.Reservations;

            return Ok(reservations);
        }


        [HttpGet("{reservationId}")]
        public ActionResult<ReservationDto> GetReservation(int reservationId)
        {
            try
            {
                var reservationToReturn = seedData.Reservations.FirstOrDefault(r => r.Id == reservationId);

                if (reservationToReturn == null)
                {
                    _logger.LogInformation("Reservation not found");
                    return NotFound();
                }

                return Ok(reservationToReturn);
            }
            catch(Exception ex)
            {
                _logger.LogCritical(
                    $"Exception whlie getting reservation with id {reservationId}",
                    ex);
                return StatusCode(500, "A problem happend while handling your request");
            }


        }
        [HttpPost]
        public ActionResult<ReservationDto> CreateReservation(
            int carId,
            ReservationForCreation reservation)
        {
            var car = seedData.Cars.FirstOrDefault(c => c.Id == carId);
            if (car == null) { return NotFound(); }

            var maxReservation = seedData.Reservations.Max(r => r.Id);

            var finalReservation = new ReservationDto()
            {
                Id = ++maxReservation,
                CarId = carId,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Status = 1,
                IsCanceled = false
            };

            car.ListOfReservations.Add(finalReservation);
            return Ok(finalReservation);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateReservation(
            int carId,
            int reservationId,
            ReservationForUpdating reservation)
        {
            var car = seedData.Cars.FirstOrDefault(c => c.Id == carId);
            if (car == null) { return NotFound(); }

            var reservationToEdit = car.ListOfReservations.FirstOrDefault(r => r.Id == reservationId);
            if (reservationToEdit == null) { return NotFound(); }

            reservationToEdit.StartDate = reservation.StartDate;
            reservationToEdit.EndDate = reservation.EndDate;
            return NoContent();
        }
        [HttpPatch("{reservationId}")]
        public ActionResult UpdateReservation(
            int carId,
            int reservationId,
            JsonPatchDocument<ReservationForUpdating> patchDocument)
        {
            var car = seedData.Cars.FirstOrDefault(c => c.Id == carId);
            if (car == null) { return NotFound(); }

            var reservationFromStore = car.ListOfReservations.FirstOrDefault(r => r.Id == reservationId);
            if (reservationFromStore == null) { return NotFound(); }


            var reservationToPatch =
                new ReservationForUpdating()
                {
                    StartDate = reservationFromStore.StartDate,
                    EndDate = reservationFromStore.EndDate
                };

            patchDocument.ApplyTo(reservationToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!TryValidateModel(reservationToPatch))
            {
                return BadRequest(ModelState);
            }

            reservationFromStore.StartDate = reservationToPatch.StartDate;
            reservationFromStore.EndDate = reservationToPatch.EndDate;
            return NoContent();
        }
        [HttpDelete("reservationId")]
        public ActionResult DeleteReservation(
            int carId,
            int reservationId)
        {
            var car = seedData.Cars.FirstOrDefault(c => c.Id == carId);
            if (car == null) { return NotFound(); }

            var reservationFromStore = car.ListOfReservations.FirstOrDefault(r => r.Id == reservationId);
            if (reservationFromStore == null) { return NotFound(); }

            car.ListOfReservations.Remove(reservationFromStore);
            _mailService.Send("Test Subject", "Test Message");
            return NoContent();
        }
    }
}
