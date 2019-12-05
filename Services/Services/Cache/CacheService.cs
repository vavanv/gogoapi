using System;
using System.Collections.Generic;
using Services.Models.Common;
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

        public void UpdateStopDetail(string code, string stop)
        {
            var typeId = (int)DataType.StopDetail;
            var cache = _cacheRepository.FindOneSync(c => c.Code == code && c.Type == typeId);

            if (cache == null)
            {
                cache = new Entities.Cache {Type = (int) DataType.StopDetail, Code = code, Data = stop};
            }
            else
            {
                cache.Data = stop;
            }

            _cacheRepository.Update(cache);
            _unitOfWork.SaveChanges();
        }

        public void UpdateShapes(string shapes)
        {
            var typeId = (int)DataType.Shapes;
            var cache = _cacheRepository.FindOneSync(c => c.Type == typeId);

            if (cache == null)
            {
                cache = new Entities.Cache { Type = typeId, Data = shapes };
            }
            else
            {
                cache.Data = shapes;
            }

            _cacheRepository.Update(cache);
            _unitOfWork.SaveChanges();
        }
    }
}