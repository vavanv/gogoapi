using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Models;
using Services.Models.Common;
using Services.Models.ViewModels;
using Services.Repository;
using Services.Services.Common;
using Services.UnitOfWork;

namespace Services.Services.Shape
{
    internal sealed class ShapeService : IShapeService
    {
        private readonly IRepository<Entities.Cache> _cacheRepository;
        private readonly IRepository<Entities.Shape> _shapeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ShapeService(IRepository<Entities.Cache> cacheRepository, IRepository<Entities.Shape> shapeRepository, IUnitOfWork unitOfWork)
        {
            _cacheRepository = cacheRepository;
            _shapeRepository = shapeRepository;
            _unitOfWork = unitOfWork;
        }

        //public async Task<ICollection<ShapeModel>> GetShapes()
        //{
        //    var cache = await _cacheRepository.FindAll(t => t.Type == (int)DataType.Shapes);
        //    var shapes = new List<ShapeModel>();

        //    foreach (var c in cache)
        //    {
        //        shapes.AddRange(JsonConvert.DeserializeObject<List<ShapeModel>>(c.Data));
        //    }

        //    return shapes;
        //}

        public async Task<ICollection<Entities.Shape>> GetShapes()
        {
            var shapes = await _shapeRepository.All();
            return shapes;
        }

        public void UpdateShapes(List<IMappingData> shapes)
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