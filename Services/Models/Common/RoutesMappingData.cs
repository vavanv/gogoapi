using System;

namespace Services.Models.Common
{
    public class RoutesMappingData : IMappingData
    {
        public string RouteId { get; set; }
        public string AgencyId { get; set; }
        public string ShotName { get; set; }
        public string LongName { get; set; }
        public int Type { get; set; }
        public string Color { get; set; }
        public string TextColor { get; set; }
    }
}