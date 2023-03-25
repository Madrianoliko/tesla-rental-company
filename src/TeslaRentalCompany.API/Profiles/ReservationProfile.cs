using AutoMapper;
using TeslaRentalCompany.Data.Entities;
using TeslaRentalCompany.Data.Models;

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
