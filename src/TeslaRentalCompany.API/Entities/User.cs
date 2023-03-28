using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeslaRentalCompany.API.Entities
{
    public class User
    {
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
            IsAdmin = false;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<Reservation> ListOfReservations { get; set; } = new List<Reservation>();

    }
}
