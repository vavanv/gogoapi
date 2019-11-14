using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Services.Repository;
using Services.Services.Common;

namespace Services.Services.Stop
{
    internal sealed class StopService : IStopService
    {
        private readonly IRepository<Entities.Cache> _cacheRepository;

        public StopService(IRepository<Entities.Cache> cacheRepository)
        {
            _cacheRepository = cacheRepository;
        }

        public async Task<ICollection<Models.StopDetail.Stop>> GetStops()
        {
            var cache = await _cacheRepository.FindAll(t => t.Type == (int) DataType.StopDetail);
            var stops = new List<Models.StopDetail.Stop>();
            foreach (var c in cache)
            {
                stops.Add(JsonConvert.DeserializeObject<Models.StopDetail.Stop>(c.Data));
            }

            return stops;
        }

        public async Task<ICollection<Models.StopDetail.Stop>> GetBusStops()
        {
            var cache = await _cacheRepository.FindAll(t => t.Type == (int)DataType.StopDetail);
            var stops = new List<Models.StopDetail.Stop>();
            foreach (var c in cache)
            {
                var o = JsonConvert.DeserializeObject<Models.StopDetail.Stop>(c.Data);
                if (o.IsBus)
                {
                    stops.Add(o);
                }
            }

            return stops;
        }

        public async Task<ICollection<Models.StopDetail.Stop>> GetTrainStops()
        {
            var cache = await _cacheRepository.FindAll(t => t.Type == (int)DataType.StopDetail);
            var stops = new List<Models.StopDetail.Stop>();
            foreach (var c in cache)
            {
                var o = JsonConvert.DeserializeObject<Models.StopDetail.Stop>(c.Data);
                if (o.IsTrain)
                {
                    stops.Add(o);
                }
            }

            return stops;
        }
    }
}