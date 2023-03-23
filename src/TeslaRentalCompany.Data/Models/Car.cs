using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeslaRentalCompany.Data.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public DateOnly YearOfManufacture { get; set; }
        public int Range { get; set; }
        public int CostPerDay { get; set; }
        public int NumberOfReservations 
        {
            get
            {
                return ListOfReservations.Count;
            }
        }

        public ICollection<Reservation> ListOfReservations { get; set; } 
            = new List<Reservation>();
    }
}
  