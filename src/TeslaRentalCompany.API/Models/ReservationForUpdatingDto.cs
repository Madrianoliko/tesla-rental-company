using System.ComponentModel.DataAnnotations;

namespace TeslaRentalCompany.API.Models
{
    public class ReservationForUpdatingDto
    {
        [Required]
        public int CarId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public int Cost { get; set; }
        [Required]
        public bool IsCanceled { get; set; }
    }
}
