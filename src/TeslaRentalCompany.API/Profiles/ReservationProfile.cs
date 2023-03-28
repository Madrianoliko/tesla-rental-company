using AutoMapper;
using TeslaRentalCompany.API.Entities;
using TeslaRentalCompany.API.Models;

namespace TeslaRentalCompany.API.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile() 
        {
            CreateMap<Reservation, ReservationDto>();
            CreateMap<ReservationForCreationDto, Reservation>();
            CreateMap<ReservationForUpdatingDto, Reservation>();
            CreateMap<Reservation, ReservationForUpdatingDto>();
        }
    }
}
