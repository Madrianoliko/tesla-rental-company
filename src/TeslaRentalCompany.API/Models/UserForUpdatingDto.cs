using System.ComponentModel.DataAnnotations;

namespace TeslaRentalCompany.API.Models
{
    public class UserForUpdatingDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
