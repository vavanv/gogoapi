using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Services.Repository;
using Services.Services.Common;
using Services.UnitOfWork;

namespace Services.Services.Cache
{
    internal sealed class CacheService : ICacheService
    {
        private readonly IRepository<Entities.Cache> _cacheRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CacheService(IRepository<Entities.Cache> cacheRepository, IUnitOfWork unitOfWork)
        {
            _cacheRepository = cacheRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<Models.StopDetail.Stop>> GetStops()
        {
            var cache = await _cacheRepository.FindAll(t => t.Type.Equals((int) DataType.StopDetail));
            var stops = new List<Models.StopDetail.Stop>();
            foreach (var c in cache) stops.Add(JsonConvert.DeserializeObject<Models.StopDetail.Stop>(c.Data));

            return stops;
        }

        public void UpdateStopDetail(string code, string stop)
        {
            const int typeId = (int) DataType.StopDetail;
            var cache = _cacheRepository.FindOneSync(c => c.Code.Equals(code) && c.Type.Equals(typeId));

            if (cache == null)
                cache = new Entities.Cache {Type = (int) DataType.StopDetail, Code = code, Data = stop};
            else
                cache.Data = stop;

            _cacheRepository.Update(cache);
            _unitOfWork.SaveChanges();
        }

        public void UpdateShapes(string shapes)
        {
            const int typeId = (int) DataType.Shapes;
            var cache = _cacheRepository.FindOneSync(c => c.Type.Equals(typeId));

            if (cache == null)
                cache = new Entities.Cache {Type = typeId, Data = shapes};
            else
                cache.Data = shapes;

            _cacheRepository.Update(cache);
            _unitOfWork.SaveChanges();
        }
    }
}