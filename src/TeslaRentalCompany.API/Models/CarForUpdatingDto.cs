﻿using System.ComponentModel.DataAnnotations;

namespace TeslaRentalCompany.API.Models
{
    public class CarForUpdatingDto
    {
        [Required]
        [MaxLength(100)]
        public string? Model { get; set; }
        [Required]
        public int CarDealershipId { get; set; }
        [Required]
        public DateTime DateOfManufacture { get; set; }
        [Required]
        public int Range { get; set; }
        [Required]
        public double DisperseHundreds { get; set; }
        [Required]
        public int TopSpeed { get; set; }
        [Required]
        public int HorsePower { get; set; }
        [Required]
        public int CostPerDay { get; set; }
    }
}
