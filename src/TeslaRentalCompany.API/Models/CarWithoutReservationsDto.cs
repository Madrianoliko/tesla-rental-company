﻿namespace TeslaRentalCompany.API.Models
{
    public class CarWithoutReservationsDto
    {
        public int Id { get; set; }
        public int CarDealershipId { get; set; }
        public string? Model { get; set; }
        public DateTime DateOfManufacture { get; set; }
        public int Range { get; set; }
        public double DisperseHundreds { get; set; }
        public double TopSpeed { get; set; }
        public int HorsePower { get; set; }
        public int CostPerDay { get; set; }
    }
}
