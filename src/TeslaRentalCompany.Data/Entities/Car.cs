using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.Data.Entities
{
    public class Car
    {
        public Car(string model)
        {
            Model = model;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "You should provide model name")]
        [MaxLength(100)]
        public string Model { get; set; }
        [Required]
        public DateTime YearOfManufacture { get; set; }
        [Required]
        public int Range { get; set; }
        [Required]
        public int CostPerDay { get; set; }


        public ICollection<Reservation> ListOfReservations { get; set; }
            = new List<Reservation>();
    }
}
