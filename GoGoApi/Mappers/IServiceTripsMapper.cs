using System;
using System.Collections.Generic;

using Services.Models.ServiceTrains;
using Services.Models.ViewModels;

namespace GoGoApi.Mappers
{
    public interface IServiceTripsMapper
    {
        IEnumerable<TripModel> MapFrom(IEnumerable<Trip> entities);
    }
}