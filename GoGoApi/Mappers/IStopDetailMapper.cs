using System;
using System.Collections.Generic;

using GoGoApi.Models;

using Services.Models.StopDetail;

namespace GoGoApi.Mappers
{
    public interface IStopDetailMapper
    {
        IEnumerable<StopModel> MapFrom(IEnumerable<Stop> entities);
    }
}