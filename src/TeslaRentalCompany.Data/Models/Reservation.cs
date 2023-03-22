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
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public int ReservationStatus { get; set; }
        public bool IsCanceled { get; set; } = false;
    }
}
