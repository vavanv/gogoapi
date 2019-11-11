using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Services.Repository;

namespace Services.Services.Cache
{
    internal sealed class CacheService : ICacheService
    {
        private readonly IRepository<Entities.Cache> _cache;

        public CacheService(IRepository<Entities.Cache> cache)
        {
            _cache = cache;
        }

        public async Task<ICollection<Entities.Cache>> GetStops()
        {
            var stops = await _cache.FindAll(t => t.Type == 2);

            return stops;
        }
    }
}