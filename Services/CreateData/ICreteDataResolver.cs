using System;
using System.Collections.Generic;

namespace Services.CreateData
{
    public interface ICreteDataResolver
    {
        List<dynamic> BuildData(string file);
    }
}