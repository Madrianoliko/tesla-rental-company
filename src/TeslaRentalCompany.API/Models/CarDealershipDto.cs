﻿using TeslaRentalCompany.API.Entities;

namespace TeslaRentalCompany.API.Models
{
    public class CarDealershipDto
    {
        public int Id { get; set; }
        public string? Localization { get; set; }
        public ICollection<Car> ListOfCars { get; set; } = new List<Car>();
    }
}
