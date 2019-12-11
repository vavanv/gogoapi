using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Models.Common;
using Services.CreateData;

namespace GoGoApi.CreateData
{
    internal class CreateDataFactory: ICreateDataFactory
    {
        private readonly IMappingData _mappingData;

        public CreateDataFactory(MappingDataType type, string file)
        {
            if (type == MappingDataType.Routes)
            {
                _mappingData = new RoutesMappingData();
            }
            _mappingData = null;
        }


        public IMappingData Create()
        {
            return _mappingData;
        }
    }
}
