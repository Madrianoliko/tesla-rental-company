using System.ComponentModel.DataAnnotations;

namespace TeslaRentalCompany.API.Models
{
    public class UserForUpdatingDto
    {

        public UserForUpdatingDto(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
