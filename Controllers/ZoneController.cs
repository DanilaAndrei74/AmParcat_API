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
    public class ZoneController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly PrintOutputService _printOutput;
        public ZoneController(DataContext context, PrintOutputService printOutput)
        {
            _context = context;
            _printOutput = printOutput;

        }

        [HttpGet]
        public ActionResult<List<ZoneOutput>> Get()
        {
            IEnumerable<Zone> zones = _context.Zone.Where(zone => zone.Deleted == false);
            return _printOutput.Zones(zones);
        }

        [HttpPost]
        public ActionResult<ZoneOutput> Post(ZoneInput input)
        {
            var check = _context.Zone.FirstOrDefault(zone => zone.Deleted == false && zone.Name == input.Name);
            if (check != null) return BadRequest();

            var floor = _context.Floor.FirstOrDefault(floor => floor.Id == input.FloorId);
            if (floor == null) return BadRequest();

            var zone = new Zone()
            {
                Name = input.Name,
                FloorId = input.FloorId,
            };

            _context.Add(zone);
            _context.SaveChanges();

            return Ok(_printOutput.Zone(zone));
        }

        [HttpPut("{zoneId}")]
        public ActionResult<ZoneOutput> Put([FromBody] ZonePutInput input, Guid zoneId)
        {
            var zone = _context.Zone.FirstOrDefault(zone => zone.Id == zoneId);
            if (zone == null) return NotFound();

            zone.Name = input.Name ?? zone.Name;

            if (input.FloorId != null)
            {
                var floor = _context.Floor.FirstOrDefault(floor => floor.Id == input.FloorId);
                if (floor == null) return BadRequest();
                zone.FloorId = (Guid)input.FloorId;
                zone.Floor= floor;
            }

            _context.SaveChanges();

            return Ok(_printOutput.Zone(zone));
        }

        [HttpDelete("{zoneId}")]
        public ActionResult Delete(Guid floorId)
        {
            var floor = _context.Floor.FirstOrDefault(floor => floor.Id == floorId);
            if (floor == null) return NotFound();

            floor.Deleted = true;

            _context.SaveChanges();

            return NoContent();
        }
    }
}
