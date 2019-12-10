using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Services.Models.Common;
using Services.Repository;
using Services.Services.Common;
using Services.UnitOfWork;

namespace Services.Services.Stop
{
    internal sealed class StopService : IStopService
    {
        private readonly IRepository<Entities.Stop> _stopRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StopService(IRepository<Entities.Stop> stopRepository, IUnitOfWork unitOfWork)
        {
            _stopRepository = stopRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ICollection<Entities.Stop>> GetStops()
        {
            var stops = await _stopRepository.All();
            return stops;
        }

        public void UpdateStops(List<StopsMappingData> stops)
        {
            var count = 0;
            foreach (var s in stops)
            {
                var stop = new Entities.Stop
                {
                    StopId = s.StopId,
                    Name = s.Name,
                    Latitude = s.Latitude,
                    Longitude = s.Longitude,
                    ZoneId = s.ZoneId,
                    Url = s.Url,
                    Type = s.Type,
                    ParentStation = s.ParentStation,
                    WheelchairBoarding = s.WheelchairBoarding,
                    Code = s.Code
                };
                _stopRepository.Update(stop);
                count += 1;
                if (count == 10000)
                {
                    _unitOfWork.SaveChanges();
                    count = 0;
                }
            }

            _unitOfWork.SaveChanges();
        }
    }
}