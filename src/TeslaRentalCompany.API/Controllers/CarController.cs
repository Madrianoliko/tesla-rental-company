using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeslaRentalCompany.API.Entities;
using TeslaRentalCompany.API.Models;
using TeslaRentalCompany.API.Services;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/car")]
    [ApiController]
    public class CarController : ControllerBase
    {
        public CarController(ITeslaRentalCompanyRepository repository,
            IMapper mapper)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        private ITeslaRentalCompanyRepository Repository { get; set; }
        private IMapper Mapper { get; set; }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarWithoutReservationsDto>>> GetCars(
            string? model)
        {
            var carsEntities = await Repository.GetCarsAsync(model);
            return Ok(Mapper.Map<IEnumerable<CarWithoutReservationsDto>>(carsEntities));
        }

        [Authorize]
        [HttpGet("{carId}", Name = "GetCar")]
        public async Task<IActionResult> GetCarAsync(int carId,
            bool includeReservations = false)
        {
            var carEntity = await Repository.GetCarAsync(carId, includeReservations);
            if (carEntity == null) { return NotFound(); }

            if (includeReservations)
            {
                return Ok(Mapper.Map<CarDto>(carEntity));
            }
            else
            {
                return Ok(Mapper.Map<CarWithoutReservationsDto>(carEntity));
            }
        }

        [Authorize(Policy = "MustBeAdmin")]
        [HttpPost]
        public async Task<ActionResult<CarDto>> CreateCarAsync(int carDealershipId, CarForCreationDto car)
        {
            if(!await Repository.CarDealershipExistsAsync(carDealershipId)) { return NotFound(); }

            var finalCar = Mapper.Map<Car>(car);

            Repository.CreateCarAsync(finalCar, carDealershipId);

            await Repository.SaveChangesAsync();

            var createdCarToReturn = Mapper.Map<CarDto>(car);

            return CreatedAtRoute("GetCar",
                new
                {
                    carId = createdCarToReturn.Id
                },
                createdCarToReturn);
        }

        [Authorize(Policy = "MustBeAdmin")]
        [HttpPut("{carId}")]
        public async Task<ActionResult<CarDto>> UpdateCarAsync(int carId, CarForUpdatingDto car)
        {
            var carEntity = await Repository.GetCarAsync(carId, false);
            if (carEntity == null) { return NotFound(); }

            Mapper.Map(car, carEntity);

            await Repository.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Policy = "MustBeAdmin")]
        [HttpDelete("{carId}")]
        public async Task<ActionResult> DeleteCar(int carId)
        {
            var carEntity = await Repository.GetCarAsync(carId, false);
            if (carEntity == null) { return NotFound() ; }

            Repository.DeleteCar(carEntity);

            await Repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
