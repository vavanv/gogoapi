using System;

namespace Configuration
{
    public class DbStringKey
    {
        public string KeyValue { get; set; }
    }

    public class BaseUrlKey
    {
        public string KeyValue { get; set; }
    }

    public class AccessKey
    {
        public string KeyValue { get; set; }
    }

    public class ActionUrl
    {
        public string StopAll { get; set; }
        public string StopDetails { get; set; }
    }
}