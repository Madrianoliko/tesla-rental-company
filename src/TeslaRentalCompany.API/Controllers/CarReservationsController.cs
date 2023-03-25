using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeslaRentalCompany.API.Interfaces;
using TeslaRentalCompany.Data;
using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/car/reservation")]
    [ApiController]
    public class CarReservationsController : ControllerBase
    {
        private readonly IReservationDataStore reservationDataStore;

        public CarReservationsController(IReservationDataStore reservationDataStore)
        {
            this.reservationDataStore = reservationDataStore ?? throw new ArgumentNullException(nameof(reservationDataStore));
        }
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetReservationsForCar(int carId)
        {
            var car = reservationDataStore.Cars.FirstOrDefault(c => c.Id == carId);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car.ListOfReservations);
        }
        [HttpGet("{reservationId}")]
        public ActionResult<Reservation> GetReservationForCar(int carId, int reservationId)
        {
            var car = reservationDataStore.Cars.FirstOrDefault(c => c.Id == carId);

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
