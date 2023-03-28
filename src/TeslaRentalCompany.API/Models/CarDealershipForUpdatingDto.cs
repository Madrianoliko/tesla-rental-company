using System.ComponentModel.DataAnnotations;

namespace TeslaRentalCompany.API.Models
{
    public class CarDealershipForUpdatingDto
    {
        [Required]
        public string? Localization { get; set; }
    }
}
