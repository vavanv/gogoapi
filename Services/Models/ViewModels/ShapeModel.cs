using System;

namespace Services.Models.ViewModels
{
    public class ShapeModel
    {
        public string ShapeId { get; set; }
        public decimal Lon { get; set; }
        public decimal Lat { get; set; }
        public int Sec { get; set; }
    }
}