using AutoMapper;
using TeslaRentalCompany.Data.Entities;
using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.API.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarWithoutReservationsDto>();
            CreateMap<Car, CarDto>();
        }
    }
}
