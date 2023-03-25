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
        private readonly ISeedDataService seedData;

        public CarReservationsController(ISeedDataService seedData)
        {
            this.seedData = seedData ?? throw new ArgumentNullException(nameof(seedData));
        }
        [HttpGet]
        public ActionResult<IEnumerable<ReservationDto>> GetReservationsForCar(int carId)
        {
            var car = seedData.Cars.FirstOrDefault(c => c.Id == carId);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car.ListOfReservations);
        }
        [HttpGet("{reservationId}")]
        public ActionResult<ReservationDto> GetReservationForCar(int carId, int reservationId)
        {
            var car = seedData.Cars.FirstOrDefault(c => c.Id == carId);

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
