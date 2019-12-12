using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Services.Models.Common;
using Services.Models.ViewModels;

namespace Services.Services.Route
{
    public interface IRouteService
    {
        Task<ICollection<Entities.Route>> GetRoutes();
        Task<ICollection<RoutesForDropDown>> GetRoutesForDropDown();
        void UpdateRoutes(List<RoutesMappingData> shapes);
    }
}