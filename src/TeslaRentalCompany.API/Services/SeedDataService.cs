using TeslaRentalCompany.API.Services;
using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.Data
{
    public class SeedDataService : ISeedDataService
    {
        public List<ReservationDto> Reservations { get; set; }
        public List<CarDto> Cars { get; set; }
        //public static ReservationDataStore Current { get; } = new ReservationDataStore();

        public SeedDataService()
        {
            Reservations = new List<ReservationDto>()
            {
                new ReservationDto()
                {
                    Id = 1,
                    CarId = 1,
                    StartDate = new DateTime(2023, 4, 1, 13, 45, 0),
                    EndDate = new DateTime(2023, 4, 20, 13, 45, 0),
                    Status = 1,
                },
                new ReservationDto()
                {
                    Id = 2,
                    CarId = 1,
                    StartDate = new DateTime(2023, 1, 1, 13, 45, 0),
                    EndDate = new DateTime(2023, 1, 10, 13, 45, 0),
                    Status = 3,
                },
                new ReservationDto()
                {
                    Id = 3,
                    CarId = 2,
                    StartDate = new DateTime(2023, 3, 20, 13, 45, 0),
                    EndDate = new DateTime(2023, 3, 21, 13, 45, 0),
                    Status = 2
                },
                new ReservationDto()
                {
                    Id = 4,
                    CarId = 2,
                    StartDate = new DateTime(2023, 3, 10, 13, 45, 0),
                    EndDate = new DateTime(2023, 3, 15, 13, 45, 0),
                    Status = 3
                },
            };
            Cars = new List<CarDto>()
            {
                new CarDto()
                {
                    Id = 1,
                    Model = "X",
                    Range = 400,
                    YearOfManufacture = new DateTime(2020, 5, 3, 0, 0, 0),
                    CostPerDay = 300,
                },
                new CarDto()
                {
                    Id = 2,
                    Model = "Y",
                    Range = 300,
                    YearOfManufacture = new DateTime(2019, 1, 2, 0, 0, 0),
                    CostPerDay = 150,
                },
            };

            foreach (CarDto car in Cars)
            {

                car.ListOfReservations = Reservations.FindAll(r => r.CarId == car.Id);
            }
        }
    }
}
