using Backend.Data.Database.Context;
using Backend.Data.Database.Entity;
using Backend.Data.Models.Input;
using Backend.Data.Models.Output;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly PrintOutputService _printOutput;
        public ReservationController(DataContext context, PrintOutputService printOutput)
        {
            _context = context;
            _printOutput = printOutput;

        }

        [HttpGet]
        public ActionResult<List<ReservationOutput>> Get()
        {
            IEnumerable<Reservation> reservations = _context.Reservation.Where(reservation => reservation.Deleted == false);
            return Ok(_printOutput.Reservations(reservations));
        }

        [HttpPost]
        public ActionResult<ReservationOutput> Post(ReservationInput input)
        {
            var user = _context.User.FirstOrDefault(user => user.Id == input.UserId);
            if (user == null) return BadRequest("User cannot be null");
            var check = _context.Reservation.FirstOrDefault(reservation => reservation.Deleted == false && reservation.UserId== input.UserId);
            if (check != null) return BadRequest("Only one reservation per user");

            var parkingSpot = _context.ParkingSpot.FirstOrDefault(spot => spot.Id == input.ParkingSpotId);
            if (parkingSpot == null) return BadRequest();

            var reservation = new Reservation()
            {
                UserId = input.UserId,
                ParkingSpot = parkingSpot,
                ParkingSpotId = input.ParkingSpotId,
                Date = input.Date,
            };

            _context.Add(reservation);
            _context.SaveChanges();

            return Ok(_printOutput.Reservation(reservation));
        }

        [HttpPut("{reservationId}")]
        public ActionResult<ReservationOutput> Put([FromBody] ReservationPutInput input, Guid reservationId)
        {
            var reservation = _context.Reservation.FirstOrDefault(reservation => reservation.Id == reservationId);
            if (reservation == null) return NotFound("Reservation not found");

            if (input.UserId != null)
            {
                var user = _context.User.FirstOrDefault(user => user.Id == input.UserId);
                if (user == null) return BadRequest("User not found");
                reservation.User = user;
                reservation.UserId = (Guid)input.UserId;
            }

            if(input.ParkingSpotId != null)
            {
                var parkingSpot = _context.ParkingSpot.FirstOrDefault(parkingSpot => parkingSpot.Id == input.ParkingSpotId);
                if (parkingSpot == null) return NotFound("ParkingSpot not found");
                reservation.ParkingSpot = parkingSpot;
                reservation.ParkingSpotId = (Guid)input.ParkingSpotId;
            }

            reservation.Date = input.Date ?? DateTime.Now;
          

            _context.SaveChanges();

            return Ok(_printOutput.Reservation(reservation));
        }

        [HttpDelete("{reservationId}")]
        public ActionResult Delete(Guid reservationId)
        {
            var reservation = _context.Reservation.FirstOrDefault(reservation => reservation.Id == reservationId);
            if (reservation == null) return NotFound();

            reservation.Deleted = true;

            _context.SaveChanges();

            return NoContent();
        }
    }
}
