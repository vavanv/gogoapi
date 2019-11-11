using System;
using System.Collections.Generic;

using Services.Models.Common;

namespace Services.Models.StopDetail
{
    public class StopDetail
    {
        public Metadata Metadata { get; set; }
        public Stop Stop { get; set; }
    }

    public class Facility
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string DescriptionFr { get; set; }
    }

    public class Parking
    {
        public string Name { get; set; }
        public string NameFr { get; set; }
        public int ParkSpots { get; set; }
        public string Type { get; set; }
    }

    public class Place
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string Radius { get; set; }
        public object Stops { get; set; }
    }

    public class Stop
    {
        public string ZoneCode { get; set; }
        public string StreetNumber { get; set; }
        public string Intersection { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public string Code { get; set; }
        public string StopName { get; set; }
        public string StopNameFr { get; set; }
        public bool IsBus { get; set; }
        public bool IsTrain { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public string DrivingDirections { get; set; }
        public object DrivingDirectionsFr { get; set; }
        public object BoardingInfo { get; set; }
        public object BoardingInfoFr { get; set; }
        public string TicketSales { get; set; }
        public string TicketSalesFr { get; set; }
        public List<Facility> Facilities { get; set; }
        public List<Parking> Parkings { get; set; }
        public Place Place { get; set; }
    }
}