using System;
using System.Collections.Generic;

using AutoMapper;
using Services.Models;
using Services.Models.StopDetail;
using Services.Models.ViewModels;

namespace GoGoApi.Mappers
{
    public class StopDetailMapper : IStopDetailMapper
    {
        private readonly IMapper _mapper;

        public StopDetailMapper()
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<Stop, StopModel>();
                configuration.CreateMap<FacilityModel, Facility>()
                    .ForMember(i => i.Code, c => c.Ignore())
                    .ForMember(i => i.DescriptionFr, c => c.Ignore());
                configuration.CreateMap<Facility, FacilityModel>()
;
                configuration.CreateMap<Parking, ParkingModel>();

                configuration.CreateMap<ParkingModel, Parking>()
                    .ForMember(i => i.NameFr, c => c.Ignore())
                    .ForMember(i => i.Type, c => c.Ignore());

                configuration.CreateMap<StopModel, Stop>()
                    .ForMember(i => i.Facilities, c => c.MapFrom(s => s.Facilities))
                    .ForMember(i => i.Parkings, c => c.MapFrom(s => s.Parkings))

                    .ForMember(i => i.ZoneCode, c => c.Ignore())
                    .ForMember(i => i.StopNameFr, c => c.Ignore())
                    .ForMember(i => i.DrivingDirectionsFr, c => c.Ignore())
                    .ForMember(i => i.BoardingInfo, c => c.Ignore())
                    .ForMember(i => i.BoardingInfoFr, c => c.Ignore())
                    .ForMember(i => i.TicketSales, c => c.Ignore())
                    .ForMember(i => i.TicketSalesFr, c => c.Ignore())
                    .ForMember(i => i.Place, c => c.Ignore());
            });

            mapperConfiguration.AssertConfigurationIsValid();

            _mapper = mapperConfiguration.CreateMapper();
        }

        public IEnumerable<StopModel> MapFrom(IEnumerable<Stop> entities)
        {
            return _mapper.Map<List<StopModel>>(entities);
        }
    }
}