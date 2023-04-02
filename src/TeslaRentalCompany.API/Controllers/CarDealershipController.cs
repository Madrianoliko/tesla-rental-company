using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeslaRentalCompany.API.Entities;
using TeslaRentalCompany.API.Models;
using TeslaRentalCompany.API.Services;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/cardealership")]
    [ApiController]
    public class CarDealershipController : ControllerBase
    {
        public CarDealershipController(ITeslaRentalCompanyRepository repository,
            IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public ITeslaRentalCompanyRepository Repository { get; }
        public IMapper Mapper { get; }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDealershipWithoutCarsDto>>> GetCarDealershipsAsync()
        {
            var carDealershipEntities = await Repository.GetCarDealershipsAsync();
            return Ok(Mapper.Map<IEnumerable<CarDealershipWithoutCarsDto>>(carDealershipEntities));
        }

        [Authorize]
        [HttpGet("{carDealershipId}", Name = "GetCarDealership")]
        public async Task<ActionResult<CarDealershipDto>> GetCarDealershipAsync(int carDealershipId,
            bool includeCars = false)
        {
            var carDealershipEntity = await Repository.GetCarDealershipAsync(carDealershipId, includeCars);
            if (carDealershipEntity == null) { return NotFound(); }

            if (includeCars)
            {
                return Ok(Mapper.Map<CarDealershipDto>(carDealershipEntity));
            }
            else
            {
                return Ok(Mapper.Map<CarDealershipWithoutCarsDto>(carDealershipEntity));
            }
        }

        [Authorize(Policy = "MustBeAdmin")]
        [HttpPost]
        public async Task<ActionResult<CarDealershipDto>> CreateCarDealershipAsync(CarDealershipForCreationDto carDealership)
        {
            if (await Repository.CarDealershipExistsAsync(carDealership.Localization)) { return BadRequest(); }

            var finalCarDealership = Mapper.Map<CarDealership>(carDealership);

            Repository.CreateCarDealership(finalCarDealership);

            await Repository.SaveChangesAsync();

            var createdCarDealershipToReturn = Mapper.Map<CarDealershipDto>(finalCarDealership);

            return CreatedAtRoute("GetCarDealership",
                new
                {
                    carDealershipId = createdCarDealershipToReturn.Id
                },
                createdCarDealershipToReturn);
        }

        [Authorize(Policy = "MustBeAdmin")]
        [HttpPut("{carDealershipId}")]
        public async Task<ActionResult<CarDealershipDto>> UpdateCarDealershipAsync(int carDealershipId, CarDealershipForUpdatingDto carDealership)
        {
            var carDealershipEntity = await Repository.GetCarDealershipAsync(carDealershipId, false);
            if (carDealershipEntity == null) { return NotFound(); }

            Mapper.Map(carDealership, carDealershipEntity);

            await Repository.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Policy = "MustBeAdmin")]
        [HttpDelete("{carDealershipId}")]
        public async Task<ActionResult> DeleteCarDealership(int carDealershipId)
        {
            var carDealershipEntity = await Repository.GetCarDealershipAsync(carDealershipId, false);
            if (carDealershipEntity == null) { return NotFound(); }

            Repository.DeleteCarDealership(carDealershipEntity);

            await Repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
