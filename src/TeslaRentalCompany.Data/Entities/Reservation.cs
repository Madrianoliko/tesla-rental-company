using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeslaRentalCompany.Data.Entities
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("CarId")]
        public Car? Car { get; set; }
        public int CarId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        // TODO obliczanie kosztu za pomocą obliczania dni razy koszt per dzien samochodu
        //public int Cost { get; set; }
        public bool IsCanceled { get; set; }
    }
}
