using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Services.Models.Common;

namespace Services.Services.Shape
{
    public interface IShapeService
    {
        Task<ICollection<Entities.Shape>> GetShapes();
        void UpdateShapes(List<ShapesMappingData> shapes);
    }
}