using Backend.Data.Database.Context;
using Backend.Data.Database.Entity;
using Backend.Data.Models.Input;
using Backend.Data.Models.Output;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingSpotController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly PrintOutputService _printOutput;
        public ParkingSpotController(DataContext context, PrintOutputService printOutput)
        {
            _context = context;
            _printOutput = printOutput;

        }

        [HttpGet]
        public ActionResult<List<ParkingSpotOutput>> Get()
        {
            IEnumerable<ParkingSpot> parkingSpots = _context.ParkingSpot.Where(parkingSpot => parkingSpot.Deleted == false);
            return Ok(_printOutput.ParkingSpots(parkingSpots));
        }

        [HttpPost]
        public ActionResult<ParkingSpotOutput> Post(ParkingSpotInput input)
        {
            var check = _context.ParkingSpot.FirstOrDefault(parkingSpot => parkingSpot.Deleted == false && parkingSpot.Name == input.Name);
            if (check != null) return BadRequest();

            var zone = _context.Zone.FirstOrDefault(zone => zone.Id == input.ZoneId);
            if (zone == null) return BadRequest();

            var parkingSpot = new ParkingSpot()
            {
                Name = input.Name,
                ZoneId = input.ZoneId,
            };

            _context.Add(parkingSpot);
            _context.SaveChanges();

            return Ok(_printOutput.ParkingSpot(parkingSpot));
        }

        [HttpPut("{parkingSpotId}")]
        public ActionResult<ParkingSpotOutput> Put([FromBody] ParkingSpotPutInput input, Guid parkingSpotId)
        {
            var parkingSpot = _context.ParkingSpot.FirstOrDefault(parkingSpot => parkingSpot.Id == parkingSpotId);
            if (parkingSpot == null) return NotFound();

            parkingSpot.Name = input.Name ?? parkingSpot.Name;

            if (input.ZoneId != null)
            {
                var zone = _context.Zone.FirstOrDefault(zone => zone.Id == input.ZoneId);
                if (zone == null) return BadRequest();
                parkingSpot.ZoneId = (Guid)input.ZoneId;
                parkingSpot.Zone = zone;
            }

            _context.SaveChanges();

            return Ok(_printOutput.ParkingSpot(parkingSpot));
        }

        [HttpDelete("{parkingSpotId}")]
        public ActionResult Delete(Guid parkingSpotId)
        {
            var parkingSpot = _context.ParkingSpot.FirstOrDefault(parkingSpot => parkingSpot.Id == parkingSpotId);
            if (parkingSpot == null) return NotFound();

            parkingSpot.Deleted = true;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("test/{zoneId}")]
        public ActionResult<List<ParkingSpotOutput>> GetParkingSpotByZoneId(Guid zoneId)
        {
            IEnumerable<ParkingSpot> parkingSpots = _context.ParkingSpot.Where(parkingSpot => parkingSpot.ZoneId == zoneId);
            return Ok(_printOutput.ParkingSpots(parkingSpots));
        }

        [HttpGet("{zoneId}")]
        public ActionResult<List<ParkingSpotWithReservationOutput>> GetParkingSpotWithReservationByZoneId(Guid zoneId, [FromQuery] DateTime date)
        {
            IEnumerable<ParkingSpot> parkingSpots = _context.ParkingSpot.Where(parkingSpot => parkingSpot.Deleted == false && parkingSpot.ZoneId == zoneId);
            IEnumerable<Reservation> reservations = _context.Reservation.Where(reservation => reservation.Deleted == false && reservation.Date == date);
            var parkingSpotsWithReservation = new List<ParkingSpotWithReservation>();
            foreach(var parkingSpot in parkingSpots)
            {
                foreach (var reservation in reservations)
                {
                    var reservationAddOn = new ReservationAddOn();
                    if (reservation.ParkingSpotId == parkingSpot.Id) reservationAddOn = new ReservationAddOn
                    {
                        UserId = reservation.UserId,
                        ReservationId = reservation.UserId,
                        Name = reservation.User.FirstName + " " + reservation.User.LastName,
                    };
                    parkingSpotsWithReservation.Add(new ParkingSpotWithReservation { ParkingSpot = parkingSpot, Reservation = reservationAddOn });
                }
            }

            return Ok(_printOutput.ParkingSpotsWithReservation(parkingSpotsWithReservation));
        }
    }
}
