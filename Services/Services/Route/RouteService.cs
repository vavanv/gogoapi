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
            var routes = await _routeRepository.FindAll(r => r.Type == 2);
            var routeIds = routes.Select(r => r.RouteId);
            var trips = await _tripRepository.FindAll(t => routeIds.Contains(t.RouteId));
            var result = (from r in routes
                join t in trips on r.RouteId equals t.RouteId
                select new RoutesForDropDown
                {
                    Key = r.ShortName + t.ShapeId,
                    ShortName = r.ShortName,
                    LongName = r.LongName,
                    Color = r.Color,
                    HeadSign = t.HeadSign,
                    ShapeId = t.ShapeId
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
            foreach (var r in routes)
            {
                var route = new Entities.Route
                {
                    RouteId = r.RouteId,
                    AgencyId = r.AgencyId,
                    ShortName = r.ShotName,
                    LongName = r.LongName,
                    Type = r.Type,
                    Color = r.Color,
                    TextColor = r.TextColor
                };
                _routeRepository.Update(route);
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
            return x.Key == y.Key;
        }

        public int GetHashCode(RoutesForDropDown obj)
        {
            return obj.Key.GetHashCode();
        }
    }
}