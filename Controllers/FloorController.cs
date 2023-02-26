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
    public class FloorController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly PrintOutputService _printOutput;
        public FloorController(DataContext context, PrintOutputService printOutput)
        {
            _context = context;
            _printOutput = printOutput;

        }

        [HttpGet]
        public ActionResult<List<FloorOutput>> Get()
        {
            IEnumerable<Floor> floors = _context.Floor.Where(floor => floor.Deleted == false);
            return _printOutput.Floors(floors);
        }

        [HttpPost]
        public ActionResult<FloorOutput> Post(FloorInput input)
        {
            var check = _context.Floor.FirstOrDefault(floor => floor.Deleted == false && floor.Name == input.Name);
            if (check != null) return BadRequest();

            var building = _context.Building.FirstOrDefault(building => building.Id == input.BuildingId);
            if (building == null) return BadRequest();

            var floor = new Floor()
            {
                Name = input.Name,
                BuildingId = input.BuildingId,
                Building = building,
            };

            _context.Add(floor);
            _context.SaveChanges();

            return Ok(_printOutput.Floor(floor));
        }

        [HttpPut("{floorId}")]
        public ActionResult<FloorOutput> Put([FromBody] FloorPutInput input, Guid floorId)
        {
            var floor = _context.Floor.FirstOrDefault(floor => floor.Id == floorId);
            if (floor == null) return NotFound();

            floor.Name = input.Name ?? floor.Name;

            if (input.BuildingId != null)
            {
                var building = _context.Building.FirstOrDefault(building => building.Id == input.BuildingId);
                if (building == null) return BadRequest();
                floor.BuildingId = (Guid)input.BuildingId;
                floor.Building = building;
            }

            _context.SaveChanges();

            return Ok(_printOutput.Floor(floor));
        }

        [HttpDelete("{floorId}")]
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
