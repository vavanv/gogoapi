using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Models;
using Services.Models.Common;
using Services.Models.ViewModels;
using Services.Repository;
using Services.Services.Common;
using Services.Services.Route;
using Services.UnitOfWork;

namespace Services.Services.Route
{
    internal sealed class RouteService : IRouteService
    {
        private readonly IRepository<Entities.Route> _routeRepository;
        private readonly IRepository<Entities.Trip> _tripRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RouteService(IRepository<Entities.Route> routeRepository, IRepository<Entities.Trip> tripRepository, IUnitOfWork unitOfWork)
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

        public async void GetRoutesForDropDown()
        {
            var routes = await _routeRepository.FindAll(r => r.Type == 2);
            var trips = await _tripRepository.All();
            var rr = (from r in routes
                join t in trips on r.RouteId equals t.RouteId
                select new
                {
                    Id = t.Id,
                    ShorName = r.ShortName, 
                    LongName = r.LongName, 
                    Color = r.Color, 
                    HeadSign = t.HeadSign,
                    ShapeId = t.ShapeId
                }).Distinct().ToList();
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
                    AgencyId =  r.AgencyId,
                    ShortName = r.ShotName,
                    LongName =  r.LongName,
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
}