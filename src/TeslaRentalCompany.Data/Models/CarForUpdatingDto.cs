using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeslaRentalCompany.Data.Models
{
    public class CarForUpdatingDto
    {
        [Required(ErrorMessage = "You should provide model name")]
        [MaxLength(100)]
        public string? Model { get; set; }
        [Required]
        public DateTime YearOfManufacture { get; set; }
        [Required]
        public int Range { get; set; }
        [Required]
        public int CostPerDay { get; set; }
    }
}
