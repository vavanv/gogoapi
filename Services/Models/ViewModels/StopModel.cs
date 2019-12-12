using System;
using System.Collections.Generic;

namespace Services.Models.ViewModels
{
    public class StopModel
    {
        public string Code { get; set; }
        public string StopName { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public bool IsBus { get; set; }
        public bool IsTrain { get; set; }
        public string StreetNumber { get; set; }
        public string Intersection { get; set; }
        public string DrivingDirections { get; set; }

        public List<FacilityModel> Facilities { get; set; }
        public List<ParkingModel> Parkings { get; set; }
    }

    public class FacilityModel
    {
        public string Description { get; set; }
    }

    public class ParkingModel
    {
        public string Name { get; set; }
        public int ParkSpots { get; set; }
    }
}