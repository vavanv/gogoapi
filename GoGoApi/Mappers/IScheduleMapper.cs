using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Models.ScheduleTrain;
using Services.Models.ViewModels;

namespace GoGoApi.Mappers
{
    public interface IScheduleMapper
    {
        IEnumerable<LineModel> MapFrom(IEnumerable<Line> entities);
    }
}
