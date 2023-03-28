using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeslaRentalCompany.API.Entities
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("CarDealershipId")]
        public CarDealership? CarDealership { get; set; }
        public int CarDealershipId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Model { get; set; }

        [Required]
        public DateTime DateOfManufacture { get; set; }

        [Required]
        public int Range { get; set; }

        [Required]
        public double DisperseHundreds { get; set; }

        [Required]
        public int TopSpeed { get; set; }

        [Required]
        public int HorsePower { get; set; }

        [Required]
        public int CostPerDay { get; set; }

        public ICollection<Reservation> ListOfReservations { get; set; }
            = new List<Reservation>();
    }
}
