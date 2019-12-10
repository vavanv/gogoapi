using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services.Cache
{
    public interface ICacheService
    {
        Task<ICollection<Models.StopDetail.Stop>> GetStops();
        void UpdateStopDetail(string code, string stop);
        void UpdateShapes(string shapes);
    }
}