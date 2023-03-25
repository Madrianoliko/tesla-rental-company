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
        private readonly ISeedDataService seedData;

        public CarController(ISeedDataService seedData)
        {
            this.seedData = seedData ?? throw new ArgumentNullException(nameof(seedData));
        }
        [HttpGet]
        public ActionResult<List<CarDto>> GetCars ()
        {
            var cars = seedData.Cars;

            return Ok(cars);
        }
        [HttpGet("{carId}")]
        public ActionResult<CarDto> GetCar (int carId)
        {
            var car = seedData.Cars.FirstOrDefault(x => x.Id == carId);
            
            if (car == null)
            {
                return NotFound();
            }
            return Ok(car);
        }
    }
}
