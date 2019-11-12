using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services.Cache
{
    public interface ICacheService
    {
        void UpdateCache(string code, string stop);
    }
}