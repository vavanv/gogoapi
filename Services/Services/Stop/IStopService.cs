using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services.Stop
{
    public interface IStopService
    {
        Task<ICollection<Models.StopDetail.Stop>> GetStops();
        Task<ICollection<Models.StopDetail.Stop>> GetBusStops();
        Task<ICollection<Models.StopDetail.Stop>> GetTrainStops();
    }
}