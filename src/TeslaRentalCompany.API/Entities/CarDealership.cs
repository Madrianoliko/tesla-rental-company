﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeslaRentalCompany.API.Entities
{
    public class CarDealership
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? Localization { get; set; }

        public ICollection<Car> ListOfCars { get; set; } = new List<Car>();
    }
}