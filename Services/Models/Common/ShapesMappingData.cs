using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.Common
{
    public class ShapesMappingData
    {
        public string ShapeId { get; set; }
        public decimal Lat { get; set; }
        public decimal Lon { get; set; }
        public int Sec { get; set; }
    }
}
