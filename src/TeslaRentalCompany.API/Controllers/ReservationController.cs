using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TeslaRentalCompany.Data;
using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetReservations()
        {
            var reservations = ReservationDataStore.Current.Reservations;

            return Ok(reservations);
        }


        [HttpGet("{id}")]
        public ActionResult<Reservation> GetReservation(int id)
        {
            var reservationToReturn = ReservationDataStore.Current.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservationToReturn == null)
            {
                return NotFound();
            }

            return Ok(reservationToReturn);

        }
        [HttpPost]
        public ActionResult<Reservation> CreateReservation(
            int carId,
            ReservationForCreation reservation)
        {
            var car = ReservationDataStore.Current.Cars.FirstOrDefault(c => c.Id == carId);
            if (car == null) { return NotFound(); }

            var maxReservation = ReservationDataStore.Current.Reservations.Max(r => r.Id);

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
            var car = ReservationDataStore.Current.Cars.FirstOrDefault(c => c.Id == carId);
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
            var car = ReservationDataStore.Current.Cars.FirstOrDefault(c => c.Id == carId);
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
            var car = ReservationDataStore.Current.Cars.FirstOrDefault(c => c.Id == carId);
            if (car == null) { return NotFound(); }

            var reservationFromStore = car.ListOfReservations.FirstOrDefault(r => r.Id == reservationId);
            if (reservationFromStore == null) { return NotFound(); }

            car.ListOfReservations.Remove(reservationFromStore);
            return NoContent();
        }
    }
}
