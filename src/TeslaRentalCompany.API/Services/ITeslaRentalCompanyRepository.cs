using TeslaRentalCompany.API.Entities;

namespace TeslaRentalCompany.API.Services
{
    public interface ITeslaRentalCompanyRepository
    {
        //Car Methods
        Task<IEnumerable<Car>> GetCarsAsync();
        Task<IEnumerable<Car>> GetCarsAsync(string? model);
        Task<Car?> GetCarAsync(int carId, bool includeReservations);
        Task<IEnumerable<Reservation>> GetReservationsForCarAsync(int carId);
        Task<bool> CarExistsAsync(int carId);

        //Rervation Methods
        Task<Reservation?> GetReservationForCarAsync(int carId,
            int reservationId);
        Task AddReservationForCarAsync(int carId, Reservation reservation);
        void DeleteReservationForCar(Reservation reservation);

        //User Methods
        Task<bool> UserExistsAsync(string userName);
        Task<bool> UserExistsAsync(int userId);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserAsync(int userId);
        void CreateUserAsync(User user);

        //Addintional Methods
        Task<User?> ValidateCredentialsAsync(string userName, string password);
        Task<bool> IsAuthorizedAsync(string userIdClaim);
        Task<bool> SaveChangesAsync();

    }
}
