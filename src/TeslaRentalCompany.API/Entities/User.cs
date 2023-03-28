using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeslaRentalCompany.API.Entities
{
    public class User
    {
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
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
        public bool IsAdmin { get; set; } = false;
        public ICollection<Reservation> ListOfReservations { get; set; }
            = new List<Reservation>();
    }
}
