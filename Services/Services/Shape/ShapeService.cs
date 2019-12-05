using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Services.Models;
using Services.Models.ViewModels;
using Services.Repository;
using Services.Services.Common;

namespace Services.Services.Shape
{
    internal sealed class ShapeService : IShapeService
    {
        private readonly IRepository<Entities.Cache> _cacheRepository;

        public ShapeService(IRepository<Entities.Cache> cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<ICollection<ShapeModel>> GetShapes()
        {
            var cache = await _cacheRepository.FindAll(t => t.Type == (int)DataType.Shapes);
            var shapes = new List<ShapeModel>();

            foreach (var c in cache)
            {
                shapes.AddRange(JsonConvert.DeserializeObject<List<ShapeModel>>(c.Data));
            }

            return shapes;
        }
    }
}