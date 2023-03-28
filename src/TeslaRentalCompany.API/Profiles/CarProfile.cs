using AutoMapper;
using TeslaRentalCompany.API.Entities;
using TeslaRentalCompany.API.Models;

namespace TeslaRentalCompany.API.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarWithoutReservationsDto>();
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();
            CreateMap<CarForCreationDto, Car>();
        }
    }
}
