using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TeslaRentalCompany.API.Entities;

namespace TeslaRentalCompany.API.Models
{
    public class CarDealershipDto
    {
        public int Id { get; set; }
        public string? Localization { get; set; }
        public ICollection<CarDto> ListOfCars { get; set; } 
            = new HashSet<CarDto>();
    }
}
