using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models.ScheduleTrain
{
    public class ScheduleTrains
    {
        public Metadata Metadata { get; set; }
        public AllLines AllLines { get; set; }
    }

    public class Metadata
    {
        public string TimeStamp { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Variant
    {
        public string Code { get; set; }
        public string Display { get; set; }
        public string Direction { get; set; }
    }

    public class Line
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsBus { get; set; }
        public bool IsTrain { get; set; }
        public List<Variant> Variant { get; set; }
    }

    public class AllLines
    {
        public List<Line> Line { get; set; }
    }
}
