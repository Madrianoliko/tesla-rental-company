using Microsoft.EntityFrameworkCore;
using TeslaRentalCompany.API.Entities;

namespace TeslaRentalCompany.API.DbContexts
{
    public class TeslaRentalCompanyContext : DbContext
    {
        public TeslaRentalCompanyContext(DbContextOptions<TeslaRentalCompanyContext> options)
            : base(options)
        {

        }
        public DbSet<Car> Cars { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<CarDealership> CarDealerships { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarDealership>()
                .HasData(
                new CarDealership
                {
                    Id = 1,
                    Localization = "Palma Airport"
                },
                new CarDealership
                {
                    Id = 2,
                    Localization = "Palma City Center"
                },
                new CarDealership
                {
                    Id = 3,
                    Localization = "Alcudia"
                },
                new CarDealership
                {
                    Id = 4,
                    Localization = "Manacor"
                });

            modelBuilder.Entity<Car>()
                .HasData(
                new Car()
                {
                    Id = 1,
                    CarDealershipId = 1,
                    Model = "S",
                    DateOfManufacture = new DateTime(2022, 1, 1, 0, 0, 0),
                    Range = 600,
                    DisperseHundreds = 2.1,
                    TopSpeed = 322,
                    HorsePower = 1020,
                    CostPerDay = 100,
                },
                new Car()
                {
                    Id = 2,
                    CarDealershipId = 2,
                    Model = "S",
                    DateOfManufacture = new DateTime(2023, 1, 1, 0, 0, 0),
                    Range = 600,
                    DisperseHundreds = 2.1,
                    TopSpeed = 322,
                    HorsePower = 1020,
                    CostPerDay = 110,
                },
                new Car()
                {
                    Id = 3,
                    CarDealershipId = 3,
                    Model = "3",
                    DateOfManufacture = new DateTime(2022, 1, 1, 0, 0, 0),
                    Range = 602,
                    DisperseHundreds = 3.3,
                    TopSpeed = 255,
                    HorsePower = 480,
                    CostPerDay = 80,
                },
                new Car()
                {
                    Id = 4,
                    CarDealershipId = 4,
                    Model = "X",
                    DateOfManufacture = new DateTime(2022, 1, 2, 0, 0, 0),
                    Range = 543,
                    DisperseHundreds = 3.4,
                    TopSpeed = 250,
                    HorsePower = 1020,
                    CostPerDay = 120,
                },
                new Car()
                {
                    Id = 5,
                    CarDealershipId = 1,
                    Model = "Y",
                    DateOfManufacture = new DateTime(2023, 1, 2, 0, 0, 0),
                    Range = 533,
                    DisperseHundreds = 5.0,
                    TopSpeed = 217,
                    HorsePower = 400,
                    CostPerDay = 90,
                });

            modelBuilder.Entity<User>()
                .HasData(
                new User("admin", "admin")
                {
                    Id = 1,
                    FirstName = "Admin",
                    LastName = "Adminowski",
                    IsAdmin = true,
                },
                new User("user1", "user1")
                {
                    Id = 2,
                    FirstName = "Marcin",
                    LastName = "Marcinowski",
                    IsAdmin = false
                },
                new User("user2", "user2")
                {
                    Id = 3,
                    FirstName = "Jan",
                    LastName = "Janowski",
                    IsAdmin = false
                });

            modelBuilder.Entity<Reservation>()
                .HasData(
                new Reservation()
                {
                    Id = 1,
                    CarId = 1,
                    UserId = 1,
                    StartDate = new DateTime(2023, 5, 1, 13, 45, 0),
                    EndDate = new DateTime(2023, 5, 20, 13, 45, 0),
                },
                new Reservation()
                {
                    Id = 2,
                    CarId = 1,
                    UserId = 1,
                    StartDate = new DateTime(2023, 3, 1, 13, 45, 0),
                    EndDate = new DateTime(2023, 3, 20, 13, 45, 0),
                    Status = 3,
                },
                new Reservation()
                {
                    Id = 3,
                    CarId = 2,
                    UserId = 2,
                    StartDate = new DateTime(2023, 5, 15, 13, 45, 0),
                    EndDate = new DateTime(2023, 5, 30, 13, 45, 0),
                },
                new Reservation()
                {
                    Id = 4,
                    CarId = 3,
                    UserId = 2,
                    StartDate = new DateTime(2023, 6, 1, 13, 45, 0),
                    EndDate = new DateTime(2023, 6, 20, 13, 45, 0),
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
