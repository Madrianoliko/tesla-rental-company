using TeslaRentalCompany.API.Entities;

namespace TeslaRentalCompany.API.Services
{
    public interface ITeslaRentalCompanyRepository
    {

        //Car Methods
        Task<IEnumerable<Car>> GetCarsAsync();
        Task<IEnumerable<Car>> GetCarsAsync(string? model);
        Task<Car?> GetCarAsync(int carId, bool includeReservations);
        Task<bool> CarExistsAsync(int carId);
        void CreateCar(Car car);
        void DeleteCar(Car car);

        //Car Dealership Methods
        Task<bool> CarDealershipExistsAsync(string localization);
        Task<IEnumerable<CarDealership>> GetCarDealershipsAsync();
        Task<CarDealership?> GetCarDealershipAsync(int carDealershipId, bool includeCars);
        void CreateCarDealership(CarDealership carDealership);
        void DeleteCarDealership(CarDealership carDealership);

        //Rervation Methods
        Task<Reservation?> GetReservationAsync(int reservationId);
        Task<IEnumerable<Reservation>> GetReservationsAsync();
        Task AddReservationAsync(int carId, int userId, Reservation reservation);
        void DeleteReservation(Reservation reservation);

        //User Methods
        Task<bool> UserExistsAsync(string userName);
        Task<bool> UserExistsAsync(int userId);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserAsync(int userId, bool includeReservations);
        void CreateUser(User user);
        void DeleteUser(User user);

        //Addintional Methods
        Task<User?> ValidateCredentialsAsync(string userName, string password);
        Task<bool> IsAuthorizedAsync(string userIdClaim);
        Task<bool> SaveChangesAsync();

    }
}
