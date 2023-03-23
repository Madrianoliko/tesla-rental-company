using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeslaRentalCompany.Data;
using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/car/reservation")]
    [ApiController]
    public class CarReservationController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetReservationsForCar(int carId)
        {
            var car = ReservationDataStore.Current.Cars.FirstOrDefault(c => c.Id == carId);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car.ListOfReservations);
        }
        [HttpGet("{reservation}")]
        public ActionResult<Reservation> GetReservationForCar(int carId, int reservationId)
        {
            var car = ReservationDataStore.Current.Cars.FirstOrDefault(c => c.Id == carId);

            if (car == null)
            {
                return NotFound();
            }

            var reservation = car.ListOfReservations.FirstOrDefault(r => r.Id == reservationId);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }
    }
}
