using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Services.Models.Common;
using Services.Repository;
using Services.UnitOfWork;

namespace Services.Services.Shape
{
    internal sealed class ShapeService : IShapeService
    {
        private readonly IRepository<Entities.Shape> _shapeRepository;
        private readonly IRepository<Entities.Route> _routeRepository;
        private readonly IRepository<Entities.Trip> _tripRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ShapeService(IRepository<Entities.Shape> shapeRepository, IRepository<Entities.Route> routeRepository,
            IRepository<Entities.Trip> tripRepository,
            IUnitOfWork unitOfWork)
        {
            _shapeRepository = shapeRepository;
            _routeRepository = routeRepository;
            _tripRepository = tripRepository;
            _unitOfWork = unitOfWork;
        }

        //select DISTINCT s.Id, s.ShapeId, s.Lon, s.Lat, s.Sec from Routes r
        //inner join Trips t on t.RouteId = r.RouteId
        //    inner join Shapes s on s.ShapeId = t.ShapeId
        //    where r.Type= 2
        //Order By s.ShapeId, s.Sec
        public async Task<ICollection<Entities.Shape>> GetTrainShapes()
        {
            var routes = await _routeRepository.FindAll(route => route.Type.Equals(2));
            var routeIds = routes.Select(route => route.RouteId);
            var trips = await _tripRepository.FindAll(trip => routeIds.Contains(trip.RouteId));
            var shapesIds = trips.Select(s => s.ShapeId).Distinct();

            var shapes = await _shapeRepository.FindAll(shape => shapesIds.Contains(shape.ShapeId));
            return shapes.Distinct().OrderBy(order => order.ShapeId).ThenBy(order => order.Sec).ToList();
        }

        public async Task<ICollection<Entities.Shape>> GetShapesByShapeId(string shapeId)
        {
            var shapes = await _shapeRepository.FindAll(shape => shape.ShapeId == shapeId, order => order.Sec);
            return shapes;
        }

        public void UpdateShapes(List<dynamic> shapes)
        {
            var count = 0;
            foreach (ShapesMappingData mapping in shapes)
            {
                var shape = new Entities.Shape
                {
                    ShapeId = mapping.ShapeId,
                    Lat = mapping.Lat,
                    Lon = mapping.Lon,
                    Sec = mapping.Sec
                };
                _shapeRepository.Update(shape);
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