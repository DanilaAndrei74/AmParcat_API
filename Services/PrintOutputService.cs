using Backend.Data.Database.Entity;
using Backend.Data.Models.Output;

namespace Backend.Services
{
    public class PrintOutputService
    {
        #region User
        public List<UserOutput> Users(IEnumerable<User> users)
        {
            var output = new List<UserOutput>();
            foreach (var user in users)
            {
                output.Add(new UserOutput
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    CreatedAt = user.CreatedAt,
                    PhotoName = user.PhotoName,
                });
            }
            return output;
        }

        public UserOutput User(User user)
        {
            return new UserOutput()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                PhotoName = user.PhotoName,
            };
        }
        #endregion
        #region Building
        public List<BuildingOutput> Buildings(IEnumerable<Building> buildings)
        {
            var output = new List<BuildingOutput>();
            foreach (var building in buildings)
            {
                output.Add(new BuildingOutput
                {
                    Id=building.Id,
                    Name = building.Name,
                    Address = building.Address,
                });
            }
            return output;
        }

        public BuildingOutput Building(Building building)
        {
            return new BuildingOutput()
            {
                Id = building.Id,
                Name = building.Name,
                Address = building.Address,
            };
        }
        #endregion
        #region Floor
        public List<FloorOutput> Floors(IEnumerable<Floor> floors)
        {
            var output = new List<FloorOutput>();
            foreach (var floor in floors)
            {
                output.Add(new FloorOutput
                {
                    Id = floor.Id,
                    Name = floor.Name,
                    BuildingId = floor.BuildingId,
                });
            }
            return output;
        }

        public FloorOutput Floor(Floor floor)
        {
            return new FloorOutput()
            {
                Id = floor.Id,
                Name = floor.Name,
                BuildingId = floor.BuildingId,
            };
        }
        #endregion
        #region Zone
        public List<ZoneOutput> Zones(IEnumerable<Zone> zones)
        {
            var output = new List<ZoneOutput>();
            foreach (var zone in zones)
            {
                output.Add(new ZoneOutput
                {
                    Id = zone.Id,
                    Name = zone.Name,
                    FloorId = zone.FloorId,
                });
            }
            return output;
        }

        public ZoneOutput Zone(Zone zone)
        {
            return new ZoneOutput()
            {
                Id = zone.Id,
                Name = zone.Name,
                FloorId = zone.FloorId,
            };
        }
        #endregion
        #region ParkingSpot
        public List<ParkingSpotOutput> ParkingSpots(IEnumerable<ParkingSpot> parkingSpots)
        {
            var output = new List<ParkingSpotOutput>();
            foreach (var parkingSpot in parkingSpots)
            {
                output.Add(new ParkingSpotOutput
                {
                    Id = parkingSpot.Id,
                    Name = parkingSpot.Name,
                    ZoneId = parkingSpot.ZoneId,
                    Status = parkingSpot.Status,
                });
            }
            return output;
        }

        public ParkingSpotOutput ParkingSpot(ParkingSpot parkingSpot)
        {
            return new ParkingSpotOutput()
            {
                Id = parkingSpot.Id,
                Name = parkingSpot.Name,
                ZoneId= parkingSpot.ZoneId,
                Status= parkingSpot.Status,
            };
        }

        public List<ParkingSpotWithReservationOutput> ParkingSpotsWithReservation(List<ParkingSpotWithReservation> parkingSpotsWithReservation)
        {
            var output = new List<ParkingSpotWithReservationOutput>();
            foreach (var parkingSpot in parkingSpotsWithReservation)
            {
                output.Add(new ParkingSpotWithReservationOutput
                {
                    Id = parkingSpot.ParkingSpot.Id,
                    Name = parkingSpot.ParkingSpot.Name,
                    ZoneId = parkingSpot.ParkingSpot.ZoneId,
                    Status = parkingSpot.ParkingSpot.Status,
                    Reservation = parkingSpot.Reservation
                });
            }
            return output;
        }
        #endregion
        #region Reservation
        public List<ReservationOutput> Reservations(IEnumerable<Reservation> reservations)
        {
            var output = new List<ReservationOutput>();
            foreach (var reservation in reservations)
            {
                output.Add(new ReservationOutput
                {
                    Id = reservation.Id,
                    ParkingSpotId = reservation.ParkingSpotId, 
                    UserId = reservation.UserId,
                    Date = reservation.Date,
                    CreatedAt = reservation.CreatedAt
                });
            }
            return output;
        }

        public ReservationOutput Reservation(Reservation reservation)
        {
            return new ReservationOutput()
            {
                Id = reservation.Id,
                ParkingSpotId = reservation.ParkingSpotId,
                UserId = reservation.UserId,
                Date = reservation.Date,
                CreatedAt = reservation.CreatedAt
            };
        }
        #endregion
    }
}
