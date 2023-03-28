using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeslaRentalCompany.API.DbContexts;
using TeslaRentalCompany.API.Entities;
using TeslaRentalCompany.API.Models;

namespace TeslaRentalCompany.API.Services
{
    public class TeslaRentalCompanyRepository : ITeslaRentalCompanyRepository
    {
        private TeslaRentalCompanyContext Context { get; set; }

        public TeslaRentalCompanyRepository(TeslaRentalCompanyContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% CARS %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public async Task<Car?> GetCarAsync(int carId, bool includeReservations)
        {
            if (includeReservations)
            {
                return await Context.Cars
                    .Include(c => c.ListOfReservations)
                    .Where(c => c.Id == carId)
                    .FirstOrDefaultAsync();
            }
            return await Context.Cars
                .Where(c => c.Id == carId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Car>> GetCarsAsync()
        {
            return await Context.Cars.ToListAsync();
        }

        public async Task<IEnumerable<Car>> GetCarsAsync(string? model)
        {
            if (string.IsNullOrEmpty(model))
            {
                return await GetCarsAsync();
            }

            model = model.Trim();
            return await Context.Cars
                .Where(c => c.Model == model)
                .OrderBy(c => c.Model)
                .ToListAsync();
        }

        public async Task<bool> CarExistsAsync(int carId)
        {
            return await Context.Cars.AnyAsync(c => c.Id == carId);
        }
        public void CreateCar(Car car)
        {
            Context.Cars.Add(car);
        }
        public void DeleteCar(Car car)
        {
            Context.Cars.Remove(car);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% CAR DEALERSHIP %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public async Task<bool> CarDealershipExistsAsync(string localization)
        {
            return await Context.CarDealerships.AnyAsync(cd => cd.Localization == localization);
        }

        public async Task<IEnumerable<CarDealership>> GetCarDealershipsAsync()
        {
            return await Context.CarDealerships.ToListAsync();
        }

        public async Task<CarDealership?> GetCarDealershipAsync(int carDealershipId, bool includeCars)
        {
            if (includeCars)
            {
                return await Context.CarDealerships
                    .Include(cd => cd.ListOfCars)
                    .Where(cd => cd.Id == carDealershipId)
                    .FirstOrDefaultAsync();
            }
            else
            {
                return await Context.CarDealerships
                .Where(cd => cd.Id == carDealershipId)
                .FirstOrDefaultAsync();
            }
        }

        public void CreateCarDealership(CarDealership carDealership)
        {
            Context.CarDealerships.Add(carDealership);
        }
        public void DeleteCarDealership(CarDealership carDealership)
        {
            Context.CarDealerships.Remove(carDealership);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% RESERVATION %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public async Task<Reservation?> GetReservationForCarAsync(int carId, int reservationId)
        {
            return await Context.Reservations
                .Where(r => r.Id == reservationId && r.CarId == carId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Reservation>> GetReservationsForCarAsync(int carId)
        {
            return await this.Context.Reservations
                .Where(r => r.CarId == carId)
                .ToListAsync();
        }

        public async Task AddReservationForCarAsync(int carId, Reservation reservation)
        {
            var car = await GetCarAsync(carId, false);
            if (car != null)
            {
                car.ListOfReservations.Add(reservation);
            }
        }

        public void DeleteReservationForCar(Reservation reservation)
        {
            Context.Reservations.Remove(reservation);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% USER %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public async Task<bool> UserExistsAsync(string userName)
        {
            return await Context.Users.AnyAsync(u => u.UserName == userName);
        }
        public async Task<bool> UserExistsAsync(int userId)
        {
            return await Context.Users.AnyAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await Context.Users.ToListAsync();
        }

        public async Task<User?> GetUserAsync(int userId, bool includeReservations)
        {
            if (includeReservations)
            {
                return await Context.Users
                    .Include(u => u.ListOfReservations)
                    .Where(u => u.Id == userId)
                    .FirstOrDefaultAsync();
            }
            else
            {
                return await Context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();
            }
        }
        public void CreateUser(User user)
        {
            Context.Users.Add(user);
        }
        public void DeleteUser(User user)
        {
            Context.Users.Remove(user);
        }

        // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% DATABASE %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

        public async Task<bool> SaveChangesAsync()
        {
            return (await Context.SaveChangesAsync() >= 0);
        }

        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%% ADDITIONAL METHODS %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        public async Task<User?> ValidateCredentialsAsync(string userName, string password)
        {
            return await Context.Users
                .Where(u => u.UserName == userName)
                .Where(u => u.Password == password)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> IsAuthorizedAsync(string userIdClaim)
        {
            int userId;
            bool success = Int32.TryParse(userIdClaim, out userId);
            if (success)
            {
                return await Context.Users.Where(u => u.Id == userId).Select(u => u.IsAdmin).FirstOrDefaultAsync();
            }
            return false;
        }


    }
}
