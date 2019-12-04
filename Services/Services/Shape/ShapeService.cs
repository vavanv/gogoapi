using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models.Common;
using Services.Repository;
using Services.UnitOfWork;

namespace Services.Services.Shape
{
    internal sealed class ShapeService : IShapeService
    {
        private readonly IRepository<Entities.Shape> _shapeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ShapeService(IRepository<Entities.Shape> shapeRepository, IUnitOfWork unitOfWork)
        {
            _shapeRepository = shapeRepository;
            _unitOfWork = unitOfWork;
        }

        public void UpdateShape(List<MappingData> shapes)
        {
            var count = 0;
            foreach (var s in shapes)
            {
                var shape = new Entities.Shape
                {
                    ShapeId = s.ShapeId,
                    Lat = s.Lat,
                    Lon = s.Lon,
                    Sec = s.Sec
                };
                _shapeRepository.Update(shape);
                count += 1;
                if (count == 1000)
                {
                    _unitOfWork.SaveChanges();
                    count = 0;
                }
            }

            _unitOfWork.SaveChanges();
        }

        public async Task<ICollection<Entities.Shape>> GetShapes()
        {
            var shapes = await _shapeRepository.All();
            return shapes;
        }
    }
}