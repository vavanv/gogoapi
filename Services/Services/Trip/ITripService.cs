using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models.Common;

namespace Services.Services.Trip
{
    public interface ITripService
    {
        Task<ICollection<Entities.Trip>> GetTrips();
        void UpdateTrips(List<TripsMappingData> trips);
    }
}