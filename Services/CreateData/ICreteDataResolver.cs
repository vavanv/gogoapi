using System;
using System.Collections.Generic;
using System.Text;
using Services.Models.Common;

namespace Services.CreateData
{
    public interface ICreteDataResolver
    {
        IList<IMappingData> BuildData(string file);
    }
}
