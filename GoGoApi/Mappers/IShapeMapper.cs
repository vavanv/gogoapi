using System;
using System.Collections.Generic;

using GoGoApi.Models;
using Services.Entities;

namespace GoGoApi.Mappers
{
    public interface IShapeMapper
    {
        IEnumerable<ShapeModel> MapFrom(IEnumerable<Shape> entities);
    }
}