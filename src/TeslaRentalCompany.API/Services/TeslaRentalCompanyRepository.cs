using Microsoft.EntityFrameworkCore;
using TeslaRentalCompany.API.DbContexts;
using TeslaRentalCompany.API.Interfaces;
using TeslaRentalCompany.Data.Entities;

namespace TeslaRentalCompany.API.Services
{
    public class TeslaRentalCompanyRepository : ITeslaRentalCompanyRepository
    {
        private readonly TeslaRentalCompanyContext _context;

        public TeslaRentalCompanyRepository(TeslaRentalCompanyContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Car?> GetCarAsync(int carId,
            bool includeReservations)
        {
            if (includeReservations)
            {
                return await _context.Cars.Include(c => c.ListOfReservations)
                    .Where(c => c.Id == carId)
                    .FirstOrDefaultAsync();
            }
            return await _context.Cars.Include(c => c.ListOfReservations)
                .Where(c => c.Id == carId)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Car>> GetCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Reservation?> GetReservationForCarAsync(int carId, int reservationId)
        {
            return await _context.Reservations.Where(r => r.Id == reservationId && r.CarId == carId)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Reservation>> GetReservationsForCarAsync(int carId)
        {
            return await this._context.Reservations
                .Where(r => r.CarId == carId).ToListAsync();
        }

    }
}
