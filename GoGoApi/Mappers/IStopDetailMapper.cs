using System;
using System.Collections.Generic;

using Services.Models.StopDetail;
using Services.Models.ViewModels;

namespace GoGoApi.Mappers
{
    public interface IStopDetailMapper
    {
        IEnumerable<StopModel> MapFrom(IEnumerable<Stop> entities);
    }
}