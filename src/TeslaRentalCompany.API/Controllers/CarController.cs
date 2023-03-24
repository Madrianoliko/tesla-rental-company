using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeslaRentalCompany.Data;
using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Car>> GetCars ()
        {
            var cars = ReservationDataStore.Current.Cars;

            return Ok(cars);
        }
        [HttpGet("{carId}")]
        public ActionResult<Car> GetCar (int carId)
        {
            var car = ReservationDataStore.Current.Cars.FirstOrDefault(x => x.Id == carId);
            
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }
    }
}
