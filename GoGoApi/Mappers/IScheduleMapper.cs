using System;
using System.Collections.Generic;

using Services.Models.ScheduleTrain;
using Services.Models.ViewModels;

namespace GoGoApi.Mappers
{
    public interface IScheduleMapper
    {
        IEnumerable<LineModel> MapFrom(IEnumerable<Line> entities);
    }
}