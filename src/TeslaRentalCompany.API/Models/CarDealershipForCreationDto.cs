using System.ComponentModel.DataAnnotations;

namespace TeslaRentalCompany.API.Models
{
    public class CarDealershipForCreationDto
    {
        [Required]
        public string? Localization { get; set; }
    }
}
