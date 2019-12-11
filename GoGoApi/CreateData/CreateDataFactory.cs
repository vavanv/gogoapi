using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Models.Common;

namespace GoGoApi.CreateData
{
    internal class CreateDataFactory: ICreateDataFactory
    {
        private readonly IMappingData _mappingData;

        public CreateDataFactory(string file)
        {
            if (file == "routes.txt")
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
