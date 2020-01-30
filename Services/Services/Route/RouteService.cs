using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Services.Models.Common;
using Services.Models.ViewModels;
using Services.Repository;
using Services.UnitOfWork;

namespace Services.Services.Route
{
    internal sealed class RouteService : IRouteService
    {
        private readonly IRepository<Entities.Route> _routeRepository;
        private readonly IRepository<Entities.Trip> _tripRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RouteService(IRepository<Entities.Route> routeRepository, IRepository<Entities.Trip> tripRepository,
            IUnitOfWork unitOfWork)
        {
            _routeRepository = routeRepository;
            _tripRepository = tripRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<Entities.Route>> GetRoutes()
        {
            var routes = await _routeRepository.All();
            return routes;
        }

        public async Task<ICollection<RoutesForDropDown>> GetRoutesForDropDown()
        {
            var routes = await _routeRepository.FindAll(route => route.Type.Equals(2));
            var routeIds = routes.Select(route => route.RouteId);
            var trips = await _tripRepository.FindAll(trip => routeIds.Contains(trip.RouteId));
            var result = (from route in routes
                join trip in trips on route.RouteId equals trip.RouteId
                select new RoutesForDropDown
                {
                    Key = route.ShortName + trip.ShapeId,
                    ShortName = route.ShortName,
                    LongName = route.LongName,
                    Color = route.Color,
                    HeadSign = trip.HeadSign,
                    ShapeId = trip.ShapeId
                });

            return result.Distinct(new ItemEqualityComparer()).ToList();
        }

        //select distinct t.id, r.ShortName, r.LongName, r.Color, t.HeadSign, t.ShapeId from Routes r
        //join Trips t on t.RouteId = r.RouteId
        //    where r.Type=2 
        //order by r.ShortName , t.ShapeId

        public void UpdateRoutes(List<RoutesMappingData> routes)
        {
            var count = 0;
            foreach (var route in routes)
            {
                var entity = new Entities.Route
                {
                    RouteId = route.RouteId,
                    AgencyId = route.AgencyId,
                    ShortName = route.ShotName,
                    LongName = route.LongName,
                    Type = route.Type,
                    Color = route.Color,
                    TextColor = route.TextColor
                };
                _routeRepository.Update(entity);
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

    class ItemEqualityComparer : IEqualityComparer<RoutesForDropDown>
    {
        public bool Equals(RoutesForDropDown x, RoutesForDropDown y)
        {
            // Two items are equal if their keys are equal.
            return y != null && x != null && x.Key == y.Key;
        }

        public int GetHashCode(RoutesForDropDown obj)
        {
            return obj.Key.GetHashCode();
        }
    }
}