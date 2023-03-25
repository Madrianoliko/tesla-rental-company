using TeslaRentalCompany.Data.Entities;

namespace TeslaRentalCompany.API.Services
{
    public interface ITeslaRentalCompanyRepository
    {
        Task<IEnumerable<Car>> GetCarsAsync();
        Task<Car?> GetCarAsync(int carId, bool includeReservations);
        Task<IEnumerable<Reservation>> GetReservationsForCarAsync(int carId);
        Task<bool> CarExistsAsync(int carId);
        Task<Reservation?> GetReservationForCarAsync(int carId,
            int reservationId);
        Task AddReservationForCarAsync(int carId, Reservation reservation);
        void DeleteReservationForCar(Reservation reservation);
        Task<bool> SaveChangesAsync();
    }
}
