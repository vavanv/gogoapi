﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<ICollection<Entities.Cache>> GetStops()
        {
            var stops = await _cacheRepository.FindAll(t => t.Type == (int) DataType.StopDetail);

            return stops;
        }

        public void UpdateCache(string code, string stop)
        {
            var cache = _cacheRepository.FindOne(c => c.Code == code).Result;
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
    }
}