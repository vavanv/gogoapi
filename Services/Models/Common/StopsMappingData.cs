using System;

namespace Services.Models.Common
{
    public class StopsMappingData : IMappingData
    {
        public string StopId { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string ZoneId { get; set; }
        public string Url { get; set; }
        public int Type { get; set; }
        public string ParentStation { get; set; }
        public bool WheelchairBoarding { get; set; }
        public string Code { get; set; }
    }
}