using System;

namespace Services.Services.Cache
{
    public interface ICacheService
    {
        void UpdateCache(string code, string stop);
    }
}