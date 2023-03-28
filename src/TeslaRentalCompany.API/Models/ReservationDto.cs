namespace TeslaRentalCompany.API.Models
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public int Cost { get; set; }
        public bool IsCanceled { get; set; }
    }
}
