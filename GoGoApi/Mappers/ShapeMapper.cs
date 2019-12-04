using System;
using System.Collections.Generic;

using AutoMapper;

using GoGoApi.Models;
using Services.Entities;

namespace GoGoApi.Mappers
{
    public class ShapeMapper : IShapeMapper
    {
        private readonly IMapper _mapper;

        public ShapeMapper()
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.CreateMap<ShapeModel, Shape>()
                    .ForMember(i => i.Id, c => c.Ignore());
            });

            mapperConfiguration.AssertConfigurationIsValid();

            _mapper = mapperConfiguration.CreateMapper();
        }

        public IEnumerable<ShapeModel> MapFrom(IEnumerable<Shape> entities)
        {
            return _mapper.Map<List<ShapeModel>>(entities);
        }
    }
}