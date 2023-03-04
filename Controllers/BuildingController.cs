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
    public class BuildingController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly PrintOutputService _printOutput;
        public BuildingController(DataContext context, PrintOutputService printOutput)
        {
            _context = context;
            _printOutput = printOutput;
         
        }

        [HttpGet]
        public ActionResult<List<BuildingOutput>> Get()
        {
            IEnumerable<Building> buildings = _context.Building.Where(building => building.Deleted == false);
            return Ok(_printOutput.Buildings(buildings));
        }

        [HttpPost]
        public ActionResult<UserOutput> Post(BuildingInput input)
        {
            var check = _context.Building.FirstOrDefault(building => building.Deleted == false && building.Name == input.Name);
            if (check != null) return BadRequest();

        
            var building = new Building()
            {
                Address = input.Address,
                Name = input.Name,
            };

            _context.Add(building);
            _context.SaveChanges();

            return Ok(_printOutput.Building(building));
        }

        [HttpPut("{buildingId}")]
        public ActionResult<BuildingOutput> Put([FromBody] BuildingPutInput input, Guid buildingId)
        {
            var building = _context.Building.FirstOrDefault(building => building.Id == buildingId);
            if (building == null) return NotFound();

            building.Name = input.Name?? building.Name;
            building.Address = input.Address ?? building.Address;
            building.Deleted = input.Deleted ?? false;

            _context.SaveChanges();

            return Ok(_printOutput.Building(building));
        }

        [HttpDelete("{buildingId}")]
        public ActionResult Delete(Guid buildingId)
        {
            var building= _context.Building.FirstOrDefault(building => building.Id == buildingId);
            if (building == null) return NotFound();

            building.Deleted = true;

            _context.SaveChanges();

            return NoContent();
        }
    }
}
