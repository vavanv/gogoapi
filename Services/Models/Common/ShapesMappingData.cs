using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.Common
{
    public class ShapesMappingData: IMappingData
    {
        public string ShapeId { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public int Sec { get; set; }
    }
}
