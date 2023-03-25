using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeslaRentalCompany.Data.Models
{
    public class CarDto
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public DateTime YearOfManufacture { get; set; }
        public int Range { get; set; }
        public int CostPerDay { get; set; }
        public int NumberOfReservations 
        {
            get
            {
                return ListOfReservations.Count;
            }
        }

        public ICollection<ReservationDto> ListOfReservations { get; set; } 
            = new List<ReservationDto>();
    }
}
  