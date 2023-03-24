using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeslaRentalCompany.Data.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        // TODO obliczanie kosztu za pomocą obliczania dni razy koszt per dzien samochodu
        //public int Cost { get; set; }
        public bool IsCanceled { get; set; }
    }
}
