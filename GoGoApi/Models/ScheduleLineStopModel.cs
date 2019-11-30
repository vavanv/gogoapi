using System;
using System.Collections.Generic;

namespace GoGoApi.Models
{
    public class ScheduleLineStopModel
    {
        public class Metadata
        {
            public string TimeStamp { get; set; }
            public string ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
        }

        public class Stop
        {
            public string Code { get; set; }
            public int Order { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public bool IsMajor { get; set; }
        }

        public class Lines
        {
            public string Code { get; set; }
            public string Direction { get; set; }
            public string Display { get; set; }
            public List<Stop> Stops { get; set; }
        }

        public class RootObject
        {
            public Metadata Metadata { get; set; }
            public Lines Lines { get; set; }
        }
    }
}