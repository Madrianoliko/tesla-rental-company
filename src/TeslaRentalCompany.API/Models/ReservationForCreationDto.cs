using System.ComponentModel.DataAnnotations;

namespace TeslaRentalCompany.API.Models
{
    public class ReservationForCreationDto
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
