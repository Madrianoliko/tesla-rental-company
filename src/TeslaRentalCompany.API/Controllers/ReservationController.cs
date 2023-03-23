using Microsoft.AspNetCore.Mvc;
using TeslaRentalCompany.Data;
using TeslaRentalCompany.Data.Models;

namespace TeslaRentalCompany.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetReservations()
        {
            var reservations = ReservationDataStore.Current.Reservations;

            return Ok(reservations);
        }


        [HttpGet("{id}")]
        public ActionResult<Reservation> GetReservation(int id)
        {
            var reservationToReturn = ReservationDataStore.Current.Reservations.FirstOrDefault(r => r.Id == id);

            if (reservationToReturn == null)
            {
                return NotFound();
            }

            return Ok(reservationToReturn);

        }
    }
}
