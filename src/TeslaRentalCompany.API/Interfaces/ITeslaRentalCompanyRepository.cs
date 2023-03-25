using TeslaRentalCompany.Data.Entities;

namespace TeslaRentalCompany.API.Interfaces
{
    public interface ITeslaRentalCompanyRepository
    {
        Task<IEnumerable<Car>> GetCarsAsync();
        Task<Car?> GetCarAsync(int carId, bool includeReservations);
        Task<IEnumerable<Reservation>> GetReservationsForCarAsync(int carId);
        Task<Reservation?> GetReservationForCarAsync(int carId,
            int reservationId);
    }
}
