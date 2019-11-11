using System;
using System.Collections.Generic;

using Services.Models.Common;

namespace Services.Models.AllStops
{
    public class AllStops
    {
        public Metadata Metadata { get; set; }
        public Stations Stations { get; set; }
    }

    public class Station
    {
        public string LocationCode { get; set; }
        public string PublicStopId { get; set; }
        public string LocationName { get; set; }
        public string LocationType { get; set; }
    }

    public class Stations
    {
        public List<Station> Station { get; set; }
    }
}