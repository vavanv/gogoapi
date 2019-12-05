using System;

namespace Services.Services.Cache
{
    public interface ICacheService
    {
        void UpdateStopDetail(string code, string stop);
        void UpdateShapes(string shapes);
    }
}