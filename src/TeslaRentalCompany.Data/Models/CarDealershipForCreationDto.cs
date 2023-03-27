using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeslaRentalCompany.Data.Models
{
    public class CarDealershipForCreationDto
    {
        [Required]
        public string? Localization { get; set; }
    }
}
