using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Services.Models.Common;
using Services.Repository;
using Services.UnitOfWork;

namespace Services.Services.Trip
{
    internal sealed class TripService : ITripService
    {
        private readonly IRepository<Entities.Trip> _tripRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TripService(IRepository<Entities.Trip> tripRepository, IUnitOfWork unitOfWork)
        {
            _tripRepository = tripRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<Entities.Trip>> GetTrips()
        {
            var shapes = await _tripRepository.All();
            return shapes;
        }

        public void UpdateTrips(List<TripsMappingData> trips)
        {
            var count = 0;
            foreach (var t in trips)
            {
                var trip = new Entities.Trip
                {
                    RouteId = t.RouteId,
                    ServiceId = t.ServiceId,
                    TripId = t.TripId,
                    HeadSign = t.HeadSign,
                    ShortName = t.ShortName,
                    DirectionId = t.DirectionId,
                    BlockId = t.BlockId,
                    ShapeId = t.ShapeId,
                    WheelchairAccessible = t.WheelchairAccessible,
                    BikesAllowed = t.BikesAllowed,
                    Variant = t.Variant
                };
                _tripRepository.Update(trip);
                count += 1;
                if (count == 10000)
                {
                    _unitOfWork.SaveChanges();
                    count = 0;
                }
            }

            _unitOfWork.SaveChanges();
        }
    }
}