using Microsoft.EntityFrameworkCore;
using TeslaRentalCompany.Data.Entities;
using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.API.DbContexts
{
    public class TeslaRentalCompanyContext : DbContext
    {
        public TeslaRentalCompanyContext(DbContextOptions<TeslaRentalCompanyContext> options)
            :base(options) 
        {
            
        }
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .HasData(
                new Car("X")
                {
                    Id = 1,
                    Model = "X",
                    Range = 400,
                    YearOfManufacture = new DateTime(2020, 5, 3, 0, 0, 0),
                    CostPerDay = 300,
                },
                new Car("Y")
                {
                    Id = 2,
                    Model = "Y",
                    Range = 300,
                    YearOfManufacture = new DateTime(2019, 1, 2, 0, 0, 0),
                    CostPerDay = 150,
                });
            modelBuilder.Entity<Reservation>()
                .HasData(
                new Reservation()
                {
                    Id = 1,
                    CarId = 1,
                    StartDate = new DateTime(2023, 4, 1, 13, 45, 0),
                    EndDate = new DateTime(2023, 4, 20, 13, 45, 0),
                    Status = 1,
                },
                new Reservation()
                {
                    Id = 2,
                    CarId = 1,
                    StartDate = new DateTime(2023, 1, 1, 13, 45, 0),
                    EndDate = new DateTime(2023, 1, 10, 13, 45, 0),
                    Status = 3,
                },
                new Reservation()
                {
                    Id = 3,
                    CarId = 2,
                    StartDate = new DateTime(2023, 3, 20, 13, 45, 0),
                    EndDate = new DateTime(2023, 3, 21, 13, 45, 0),
                    Status = 2
                },
                new Reservation()
                {
                    Id = 4,
                    CarId = 2,
                    StartDate = new DateTime(2023, 3, 10, 13, 45, 0),
                    EndDate = new DateTime(2023, 3, 15, 13, 45, 0),
                    Status = 3
                });
            modelBuilder.Entity<User>()
                .HasData(
                new UserDto("admin", "password")
                {
                    UserId = 1,
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    IsAdmin = true,
                },
                new UserDto("user", "qaz123")
                {
                    UserId = 2,
                    FirstName = "Marcin",
                    LastName = "Nowak",
                    IsAdmin = false
                });
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
