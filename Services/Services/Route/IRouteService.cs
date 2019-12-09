using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models.Common;

namespace Services.Services.Route
{
    public interface IRouteService
    {
        Task<ICollection<Entities.Route>> GetRoutes();
        void UpdateRoutes(List<RoutesMappingData> shapes);
    }
}