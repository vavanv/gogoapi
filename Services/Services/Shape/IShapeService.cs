using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services.Shape
{
    public interface IShapeService
    {
        Task<ICollection<Entities.Shape>> GetShapes();
        Task<ICollection<Entities.Shape>> GetShapesByShapeId(string shapeId);
        void UpdateShapes(List<dynamic> shapes);
    }
}