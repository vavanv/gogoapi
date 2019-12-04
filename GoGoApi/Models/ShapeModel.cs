using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGoApi.Models
{
    public class ShapeModel
    {
        public string ShapeId { get; set; }
        public decimal Lon { get; set; }
        public decimal Lat { get; set; }
        public int Sec { get; set; }
    }
}
