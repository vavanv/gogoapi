using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models;
using Services.Models.ViewModels;

namespace Services.Services.Shape
{
    public interface IShapeService
    {
        Task<ICollection<ShapeModel>> GetShapes();
    }
}