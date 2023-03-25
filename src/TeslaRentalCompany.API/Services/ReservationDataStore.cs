using TeslaRentalCompany.API.Interfaces;
using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.Data
{
    public class ReservationDataStore : IReservationDataStore
    {
        public List<Reservation> Reservations { get; set; }
        public List<Car> Cars { get; set; }
        //public static ReservationDataStore Current { get; } = new ReservationDataStore();

        public ReservationDataStore()
        {
            Reservations = new List<Reservation>()
            {
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
                },
            };
            Cars = new List<Car>()
            {
                new Car()
                {
                    Id = 1,
                    Model = "X",
                    Range = 400,
                    YearOfManufacture = new DateOnly(2020, 5, 3),
                    CostPerDay = 300,
                },
                new Car()
                {
                    Id = 2,
                    Model = "Y",
                    Range = 300,
                    YearOfManufacture = new DateOnly(2019, 1, 2),
                    CostPerDay = 150,
                },
            };

            foreach (Car car in Cars)
            {

                car.ListOfReservations = Reservations.FindAll(r => r.CarId == car.Id);
            }
        }
    }
}
