using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.Data
{
    public class ReservationDataStore
    {
        public List<Reservation> Reservations { get; set; }
        public static ReservationDataStore Current { get; } = new ReservationDataStore();

        public ReservationDataStore()
        {
            Reservations = new List<Reservation>()
            {
                new Reservation()
                {
                    Id = 1,
                    ReservationStart = new DateTime(2023, 4, 1, 13, 45, 0),
                    ReservationEnd = new DateTime(2023, 4, 20, 13, 45, 0),
                    ReservationStatus = 1
                },
                new Reservation()
                {
                    Id = 2,
                    ReservationStart = new DateTime(2023, 3, 20, 13, 45, 0),
                    ReservationEnd = new DateTime(2023, 3, 21, 13, 45, 0),
                    ReservationStatus = 2
                },
                new Reservation()
                {
                    Id = 3,
                    ReservationStart = new DateTime(2023, 3, 10, 13, 45, 0),
                    ReservationEnd = new DateTime(2023, 3, 15, 13, 45, 0),
                    ReservationStatus = 3
                },

            };
        }
    }
}
