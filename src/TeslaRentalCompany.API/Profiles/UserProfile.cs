using AutoMapper;
using TeslaRentalCompany.API.Entities;
using TeslaRentalCompany.API.Models;

namespace TeslaRentalCompany.API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserWithoutReservationsDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserForCreationDto, User>();
            CreateMap<UserForUpdatingDto, User>();
        }
    }
}
