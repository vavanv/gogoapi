using System;
using System.Collections.Generic;

using Services.Models.Common;

namespace Services.Services.Shape
{
    public interface IShapeService
    {
        void UpdateShape(List<MappingData> shapes);
    }
}