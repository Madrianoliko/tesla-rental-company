using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TeslaRentalCompany.API.Services;
using TeslaRentalCompany.Data;
using TeslaRentalCompany.Data.Entities;
using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ITeslaRentalCompanyRepository _repository;
        private readonly IMapper _mapper;

        public CarController(ITeslaRentalCompanyRepository repository,
            IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarWithoutReservationsDto>>> GetCars()
        {
            var carsEntities = await _repository.GetCarsAsync();
            return Ok(_mapper.Map<IEnumerable<CarWithoutReservationsDto>>(carsEntities));
        }
        [HttpGet("{carId}")]
        public async Task<IActionResult> GetCarAsync(int carId,
            bool includeReservations = false)
        {
            var carEntity = await _repository.GetCarAsync(carId, includeReservations);
            if (carEntity == null) { return NotFound(); }

            if (includeReservations)
            {
                return Ok(_mapper.Map<CarDto>(carEntity));
            }
            else
            {
                return Ok(_mapper.Map<CarWithoutReservationsDto>(carEntity));
            }
        }
    }
}
