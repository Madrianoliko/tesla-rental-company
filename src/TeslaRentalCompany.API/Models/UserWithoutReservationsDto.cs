namespace TeslaRentalCompany.API.Models
{
    public class UserWithoutReservationsDto
    {
        public UserWithoutReservationsDto(string userName, string password)
        {
            UserName = userName;
            Password = password;
            IsAdmin = false;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
