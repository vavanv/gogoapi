
using System.Collections.Generic;
using AutoMapper;

using GoGoApi.Models;

using Services.Models.StopDetail;

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
                configuration.CreateMap<StopModel, Stop>()
                    .ForMember(i => i.ZoneCode, c => c.Ignore())
                    .ForMember(i => i.StreetNumber, c => c.Ignore())
                    .ForMember(i => i.Intersection, c => c.Ignore())
                    .ForMember(i => i.StopNameFr, c => c.Ignore())
                    .ForMember(i => i.IsBus, c => c.Ignore())
                    .ForMember(i => i.IsTrain, c => c.Ignore())
                    .ForMember(i => i.DrivingDirections, c => c.Ignore())
                    .ForMember(i => i.DrivingDirectionsFr, c => c.Ignore())
                    .ForMember(i => i.BoardingInfo, c => c.Ignore())
                    .ForMember(i => i.BoardingInfoFr, c => c.Ignore())
                    .ForMember(i => i.TicketSales, c => c.Ignore())
                    .ForMember(i => i.TicketSalesFr, c => c.Ignore())
                    .ForMember(i => i.Facilities, c => c.Ignore())
                    .ForMember(i => i.Parkings, c => c.Ignore())
                    .ForMember(i => i.Place, c => c.Ignore());
            });

            mapperConfiguration.AssertConfigurationIsValid();

            _mapper = mapperConfiguration.CreateMapper();
        }

        public StopModel MapFrom(Stop itemEntity)
        {
            return _mapper.Map<StopModel>(itemEntity);
        }

        public Stop MapFrom(StopModel model, Stop itemEntity)
        {
            return _mapper.Map(model, itemEntity);
        }

        public IEnumerable<StopModel> MapFrom(IEnumerable<Stop> entities)
        {
            return _mapper.Map<List<StopModel>>(entities);
        }
    }
}