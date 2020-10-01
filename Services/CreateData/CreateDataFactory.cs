using System;

namespace Services.CreateData
{
    internal class CreateDataFactory : ICreateDataFactory
    {
        public ICreteDataResolver Create(MappingDataType type)
        {
            if (type == MappingDataType.Shapes) return new ShapesCreteDataResolver();
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