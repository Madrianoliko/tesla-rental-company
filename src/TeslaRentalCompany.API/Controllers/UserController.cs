﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TeslaRentalCompany.API.Entities;
using TeslaRentalCompany.API.Models;
using TeslaRentalCompany.API.Services;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController(ITeslaRentalCompanyRepository repository, IMapper mapper)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public ITeslaRentalCompanyRepository Repository { get; }
        public IMapper Mapper { get; }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserWithoutReservationsDto>>> GetUsersAsync()
        {
            var userEntities = await Repository.GetUsersAsync();
            return Ok(Mapper.Map<IEnumerable<UserWithoutReservationsDto>>(userEntities));
        }

        [HttpGet("{userId}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUserAsync(int userId)
        {
            var user = await Repository.GetUserAsync(userId);
            if (user == null) { return NotFound(); }

            return Ok(Mapper.Map<UserDto>(user));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUserAsync(UserForCreationDto user)
        {
            if (await Repository.UserExistsAsync(user.UserName)) { return BadRequest(); }

            var finalUser = Mapper.Map<User>(user);

            Repository.CreateUserAsync(finalUser);

            await Repository.SaveChangesAsync();

            var createdUserToReturn = Mapper.Map<UserDto>(finalUser);

            return CreatedAtRoute("GetUser",
                new
                {
                    userId = createdUserToReturn.Id
                },
                createdUserToReturn);
        }
        //TODO
        [HttpPut("userId")]
        public async Task<ActionResult<UserDto>> UpdateUserAsync(int userId, UserForUpdatingDto user)
        {
            if (!await Repository.UserExistsAsync(userId))
            {
                return NotFound();
            }
            return NoContent();
        }
        //public async Task<ActionResult> UpdateReservation(
        //    int carId,
        //    int reservationId,
        //    ReservationForUpdatingDto reservation)
        //{
        //    if (!await Repository.CarExistsAsync(carId))
        //    {
        //        return NotFound();
        //    }

        //    var reservationEntity = await Repository.GetReservationForCarAsync(carId, reservationId);

        //    if (reservationEntity == null)
        //    {
        //        return NotFound();
        //    }

        //    Mapper.Map(reservation, reservationEntity);

        //    await Repository.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
