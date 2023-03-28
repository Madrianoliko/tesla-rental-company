using AutoMapper;
using TeslaRentalCompany.API.Entities;
using TeslaRentalCompany.API.Models;

namespace TeslaRentalCompany.API.Profiles
{
    public class CarDealershipProfile : Profile
    {
        public CarDealershipProfile()
        {
            CreateMap<CarDealership, CarDealershipWithoutCarsDto>();
            CreateMap<CarDealership, CarDealershipDto>();
            CreateMap<CarDealershipDto, CarDealership>();
            CreateMap<CarDealershipForCreationDto, CarDealership>();
        }
    }
}
