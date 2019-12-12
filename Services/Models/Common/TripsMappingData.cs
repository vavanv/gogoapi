using System;

namespace Services.Models.Common
{
    public class TripsMappingData : IMappingData
    {
        public string RouteId { get; set; }
        public string ServiceId { get; set; }
        public string TripId { get; set; }
        public string HeadSign { get; set; }
        public string ShortName { get; set; }
        public int DirectionId { get; set; }
        public string BlockId { get; set; }
        public string ShapeId { get; set; }
        public bool WheelchairAccessible { get; set; }
        public bool BikesAllowed { get; set; }
        public string Variant { get; set; }
    }
}