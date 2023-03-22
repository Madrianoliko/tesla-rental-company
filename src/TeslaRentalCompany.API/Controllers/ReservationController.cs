using Microsoft.AspNetCore.Mvc;
using TeslaRentalCompany.Data;
using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetReservations()
        {
            return new JsonResult(ReservationDataStore.Current.Reservations);
        }
        [HttpGet("{id}")]
        public JsonResult GetReservation(int id)
        {
            return new JsonResult(
                    ReservationDataStore.Current.Reservations.FirstOrDefault(r => r.Id == id));
        }
    }
} 
