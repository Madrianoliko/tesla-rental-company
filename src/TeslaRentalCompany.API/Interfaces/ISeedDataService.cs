using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.API.Interfaces
{
    public interface ISeedDataService
    {
        List<CarDto> Cars { get; set; }
        List<ReservationDto> Reservations { get; set; }
    }
}