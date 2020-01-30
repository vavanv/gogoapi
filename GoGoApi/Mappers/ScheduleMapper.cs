using System;
using System.Collections.Generic;
using AutoMapper;
using Services.Models.ScheduleTrain;
using Services.Models.ViewModels;

namespace GoGoApi.Mappers
{
    public class ScheduleMapper : IScheduleMapper
    {
        private readonly IMapper _mapper;

        public ScheduleMapper()
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<Line, LineModel>();
                configuration.CreateMap<LineModel, Line>()
                    .ForMember(i => i.IsTrain, c => c.Ignore())
                    .ForMember(i => i.IsBus, c => c.Ignore());
                configuration.CreateMap<VariantModel, Variant>();
                configuration.CreateMap<Variant, VariantModel>();
            });

        mapperConfiguration.AssertConfigurationIsValid();

        _mapper = mapperConfiguration.CreateMapper();
        }
        public IEnumerable<LineModel> MapFrom(IEnumerable<Line> entities)
        {
            return _mapper.Map<List<LineModel>>(entities);
        }
    }
}
