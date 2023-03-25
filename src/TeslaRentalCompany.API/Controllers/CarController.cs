using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeslaRentalCompany.API.Interfaces;
using TeslaRentalCompany.Data;
using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly IReservationDataStore reservationDataStore;

        public CarController(IReservationDataStore reservationDataStore)
        {
            this.reservationDataStore = reservationDataStore ?? throw new ArgumentNullException(nameof(reservationDataStore));
        }
        [HttpGet]
        public ActionResult<List<Car>> GetCars ()
        {
            var cars = reservationDataStore.Cars;

            return Ok(cars);
        }
        [HttpGet("{carId}")]
        public ActionResult<Car> GetCar (int carId)
        {
            var car = reservationDataStore.Cars.FirstOrDefault(x => x.Id == carId);
            
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }
    }
}
