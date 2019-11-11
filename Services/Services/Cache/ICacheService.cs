using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services.Cache
{
    public interface ICacheService
    {
        Task<ICollection<Entities.Cache>> GetStops();
        void UpdateCache(string code, string stop);
    }
}