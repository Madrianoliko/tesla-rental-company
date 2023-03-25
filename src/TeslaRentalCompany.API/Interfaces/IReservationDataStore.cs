using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.API.Interfaces
{
    public interface IReservationDataStore
    {
        List<Car> Cars { get; set; }
        List<Reservation> Reservations { get; set; }
    }
}