using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeslaRentalCompany.Data.Models
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsAdmin { get; set; } = false;
        public UserDto(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
