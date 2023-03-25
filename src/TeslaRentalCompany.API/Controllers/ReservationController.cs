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
        private readonly ILogger<ReservationController> logger;
        private readonly IMailService mailService;
        private readonly IReservationDataStore reservationDataStore;

        public ReservationController(
            ILogger<ReservationController> logger,
            IMailService mailService,
            IReservationDataStore reservationDataStore
            )
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            this.reservationDataStore = reservationDataStore ?? throw new ArgumentNullException(nameof(reservationDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetReservations()
        {
            var reservations = reservationDataStore.Reservations;

            return Ok(reservations);
        }


        [HttpGet("{reservationId}")]
        public ActionResult<Reservation> GetReservation(int reservationId)
        {
            try
            {
                var reservationToReturn = reservationDataStore.Reservations.FirstOrDefault(r => r.Id == reservationId);

                if (reservationToReturn == null)
                {
                    logger.LogInformation("Reservation not found");
                    return NotFound();
                }

                return Ok(reservationToReturn);
            }
            catch(Exception ex)
            {
                logger.LogCritical(
                    $"Exception whlie getting reservation with id {reservationId}",
                    ex);
                return StatusCode(500, "A problem happend while handling your request");
            }


        }
        [HttpPost]
        public ActionResult<Reservation> CreateReservation(
            int carId,
            ReservationForCreation reservation)
        {
            var car = reservationDataStore.Cars.FirstOrDefault(c => c.Id == carId);
            if (car == null) { return NotFound(); }

            var maxReservation = reservationDataStore.Reservations.Max(r => r.Id);

            var finalReservation = new Reservation()
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
            var car = reservationDataStore.Cars.FirstOrDefault(c => c.Id == carId);
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
            var car = reservationDataStore.Cars.FirstOrDefault(c => c.Id == carId);
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
            var car = reservationDataStore.Cars.FirstOrDefault(c => c.Id == carId);
            if (car == null) { return NotFound(); }

            var reservationFromStore = car.ListOfReservations.FirstOrDefault(r => r.Id == reservationId);
            if (reservationFromStore == null) { return NotFound(); }

            car.ListOfReservations.Remove(reservationFromStore);
            mailService.Send("Test Subject", "Test Message");
            return NoContent();
        }
    }
}
