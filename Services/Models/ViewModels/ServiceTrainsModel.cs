using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.ViewModels
{
    //public class ServiceTrainsModel
    //{
        public class TripModel
        {
            public string Cars { get; set; }
            public string TripNumber { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string LineCode { get; set; }
            public string RouteNumber { get; set; }
            public string VariantDir { get; set; }
            public string Display { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public bool IsInMotion { get; set; }
            public int DelaySeconds { get; set; }
            public double Course { get; set; }
            public string FirstStopCode { get; set; }
            public string LastStopCode { get; set; }
            public string PrevStopCode { get; set; }
            public string NextStopCode { get; set; }
            public string AtStationCode { get; set; }
            public string ModifiedDate { get; set; }
        }

        //public class TripsModel
        //{
        //    public List<TripModel> Trip { get; set; }
        //}
    //}
}
