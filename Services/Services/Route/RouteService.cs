using System.Collections.Generic;
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
        private readonly IUnitOfWork _unitOfWork;

        public RouteService(IRepository<Entities.Route> routeRepository, IUnitOfWork unitOfWork)
        {
            _routeRepository = routeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<Entities.Route>> GetRoutes()
        {
            var routes = await _routeRepository.All();
            return routes;
        }

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