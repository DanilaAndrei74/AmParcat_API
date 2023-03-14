using Backend.Data.Database.Context;
using Backend.Data.Database.Entity;
using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Backend.Controllers
{
    [Route("ADMIN")]
    [ApiController]
    public class ADMIN : ControllerBase
    {
        private readonly DataContext _context;
        private readonly PasswordService _passwordService;
        public ADMIN(DataContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }
        #region SeedData
        private List<Building> buildings = new List<Building>
        {
            new Building() { Name = "Parklake", Address ="Bd. Dorel"},
            new Building() { Name = "MegaMall", Address ="Bd. Pierre de Coubertin"},
        };

        private List<Floor> floors = new List<Floor>
        {
             new Floor() { Name = "-1"},
             new Floor() { Name = "-2"},
        };

        private List<Zone> zones = new List<Zone>
        {
             new Zone() { Name = "A"},
             new Zone() { Name = "B"},
        };

        private List<User> users = new List<User>
        {
           new User() { FirstName = "Dorel", LastName = "Mitrea", Email = "DorelMitrea@gmail.com", Password = "string", }
        };

        private string Test = "Test";
        private int numberOfReservations = 40;
        private int numberOfParkingSpots = 10;
        #endregion

        #region SeedDatabase
        [HttpPost("SeedDatabase")]
        public ActionResult SeedDatabase()
        {
            //PROBLEMA E IN METODA ASTA
            foreach (var building in buildings)
            {
                SeedBuilding(building);

                foreach (var floor in floors)
                {
                    SeedFloor(building, floor);
                    foreach (var zone in zones)
                    {
                        SeedZone(floor, zone);
                        for (var i = 1; i <= numberOfParkingSpots; i++)
                        {
                            var spot = new ParkingSpot
                            {
                                Name = i.ToString(),
                                Zone = zone,
                                ZoneId = zone.Id,
                                Id = Guid.NewGuid()
                            };
                            _context.Add(spot);
                            
                        }
                    }
                }
            }
            _context.SaveChanges();

            foreach (var user in users)
            {
                var Salt = _passwordService.CreateSalt();
                var Password = _passwordService.HashPassword(user.Password, Salt);
                user.Password = Password;
                user.Salt = Salt;
                _context.Add(user);
                _context.SaveChanges();
            }

            for (var i = 1; i < 40; i++)
            {
                var testName = "Test " + i.ToString();
                var user = new User { FirstName = testName, LastName = testName, Email = "testEmail" + i.ToString() + "@gmail.com", Password = testName, Salt = Test };
                _context.Add(user);
                _context.SaveChanges();
            }


            return Created("Database seeded succesfully", null);
        }


        private void SeedBuilding(Building building)
        {
            building.Id = Guid.NewGuid();
            _context.Add(building);
        }

        private void SeedFloor (Building building, Floor floor)
        {
            floor.Id = Guid.NewGuid();
            floor.BuildingId = building.Id;
            floor.Building = building;
            _context.Add(floor);
        }

        private void SeedZone(Floor floor, Zone zone)
        {
            zone.Id = Guid.NewGuid();
            zone.Floor = floor;
            zone.FloorId = floor.Id;
            _context.Add(zone);
        }
        #endregion

        #region CreateReservations
        [HttpPost("Reservation/{date}")]
        public ActionResult CreateReservations(DateTime date)
        {
            int take = numberOfReservations;
            IEnumerable<ParkingSpot> parkingSpots = _context.ParkingSpot.Where(
                spot => spot.Deleted == false &&
                spot.Status == 0
                );
            Random rand = new Random();
            var skip = parkingSpots.Count();

            var i = 0;
            var j = 0;
            var tmpSpots = new List<ParkingSpot>();
            foreach (var spot in parkingSpots)
            {
                double probability = (take - i) / (skip - i - j);

                bool result = rand.NextDouble() < probability;
                if (result)
                {
                    i++;
                    tmpSpots.Add(spot);
                }
                else j++;
            }

            var testUsers = _context.User.Where(user => user.Salt == Test);
            foreach (var spot in tmpSpots)
            {
                if (testUsers.Count() <= 1) break;
                var tmpUser = testUsers.First();
                var reservation = new Reservation
                {
                    Date = date,
                    ParkingSpot = spot,
                    ParkingSpotId = spot.Id,
                    User = tmpUser,
                    UserId = tmpUser.Id,
                };
                testUsers = testUsers.Skip(1).Take(testUsers.Count() - 1);
                _context.Add(reservation);
                _context.SaveChanges();
            }
            return Created("Reservations created", null);
        }
        #endregion
    }
}
