using System;
using System.Collections.Generic;
using Services.Entities;
using Services.Models.ViewModels;

namespace GoGoApi.Mappers
{
    public interface IShapeMapper
    {
        IEnumerable<ShapeModel> MapFrom(IEnumerable<Shape> entities);
    }
}