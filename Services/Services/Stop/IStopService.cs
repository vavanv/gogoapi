using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models.Common;

namespace Services.Services.Stop
{
    public interface IStopService
    {
        Task<ICollection<Entities.Stop>> GetStops();
        void UpdateStops(List<StopsMappingData> stops);
    }
}