using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models;
using Services.Models.Common;
using Services.Models.ViewModels;

namespace Services.Services.Shape
{
    public interface IShapeService
    {
        Task<ICollection<Entities.Shape>> GetShapes();
        void UpdateShapes(List<MappingData> shapes);
    }
}