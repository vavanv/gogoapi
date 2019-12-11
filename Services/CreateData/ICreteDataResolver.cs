using System;
using System.Collections.Generic;
using System.Text;
using Services.Models.Common;

namespace Services.CreateData
{
    public interface ICreteDataResolver
    {
        List<IMappingData> BuildData(string file);
    }
}
