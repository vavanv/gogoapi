using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services.Cache
{
    internal interface ICacheService
    {
        Task<ICollection<Entities.Cache>> GetStops();
    }
}