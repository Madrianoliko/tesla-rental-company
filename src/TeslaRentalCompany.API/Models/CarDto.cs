namespace TeslaRentalCompany.API.Models
{
    public class CarDto
    {
        public int Id { get; set; }
        public int CarDealershipId { get; set; }
        public string? Model { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public int Range { get; set; }
        public double DisperseHundreds { get; set; }
        public int TopSpeed { get; set; }
        public int HorsePower { get; set; }
        public int CostPerDay { get; set; }
        public int NumberOfReservations
        {
            get
            {
                return ListOfReservations.Count;
            }
        }

        public ICollection<ReservationDto> ListOfReservations { get; set; }
            = new HashSet<ReservationDto>();
    }
}
