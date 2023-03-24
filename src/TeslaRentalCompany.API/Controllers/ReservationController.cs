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
    }
}
