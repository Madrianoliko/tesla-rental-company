using TeslaRentalCompany.API.Entities;

namespace TeslaRentalCompany.API.Services
{
    public interface ITeslaRentalCompanyRepository
    {
        Task<IEnumerable<Car>> GetCarsAsync();
        Task<IEnumerable<Car>> GetCarsAsync(string? model);
        Task<Car?> GetCarAsync(int carId, bool includeReservations);
        Task<IEnumerable<Reservation>> GetReservationsForCarAsync(int carId);
        Task<bool> CarExistsAsync(int carId);
        Task<Reservation?> GetReservationForCarAsync(int carId,
            int reservationId);
        Task AddReservationForCarAsync(int carId, Reservation reservation);
        void DeleteReservationForCar(Reservation reservation);
        Task<bool> UserExistsAsync(string userName);
        Task<User?> ValidateCredentialsAsync(string userName, string password);
        Task<bool> IsAuthorizedAsync(string userIdClaim);
        Task<bool> SaveChangesAsync();
    }
}
