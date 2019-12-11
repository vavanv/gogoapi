using System.Collections.Generic;
using GoGoApi.CreateData;
using Services.Models.Common;

namespace Services.CreateData
{
    internal class CreateDataFactory: ICreateDataFactory
    {

        public ICreteDataResolver Create(MappingDataType type)
        {
            if (type == MappingDataType.Shapes)
            {
                return new ShapesCreteDataResolver(new List<IMappingData>());
            }
            //else if (type == MappingDataType.Routes)
            //{
            //    _mappingData = new RoutesMappingData();
            //}
            //else if (type == MappingDataType.Stops)
            //{
            //    _mappingData = new StopsMappingData();
            //}
            //else if (type == MappingDataType.Trips)
            //{
            //    _mappingData = new TripsMappingData();
            //}
       
            //return _mappingData;
            return null;

        }
    }
}
