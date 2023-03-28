using AutoMapper;
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
        public async Task<ActionResult<UserDto>> GetUserAsync(int userId,
            bool includeReservations = false)
        {
            var userEntity = await Repository.GetUserAsync(userId, includeReservations);
            if (userEntity == null) { return NotFound(); }

            if (includeReservations)
            {
                return Ok(Mapper.Map<UserDto>(userEntity));
            }
            else
            {
                return Ok(Mapper.Map<UserWithoutReservationsDto>(userEntity));
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> CreateUserAsync(UserForCreationDto user)
        {
            if (await Repository.UserExistsAsync(user.UserName)) { return BadRequest(); }

            var finalUser = Mapper.Map<User>(user);

            Repository.CreateUser(finalUser);

            await Repository.SaveChangesAsync();

            var createdUserToReturn = Mapper.Map<UserDto>(finalUser);

            return CreatedAtRoute("GetUser",
                new
                {
                    userId = createdUserToReturn.Id
                },
                createdUserToReturn);
        }
        [HttpPut("{userId}")]
        public async Task<ActionResult<UserDto>> UpdateUserAsync(int userId, UserForUpdatingDto user)
        {
            var userEntity = await Repository.GetUserAsync(userId, false);
            if (userEntity == null) { return NotFound();  }

            Mapper.Map(user, userEntity);

            await Repository.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{userId}")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var userEntity = await Repository.GetUserAsync(userId, false);
            if (userEntity == null) { return NotFound(); }

            Repository.DeleteUser(userEntity);

            await Repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
