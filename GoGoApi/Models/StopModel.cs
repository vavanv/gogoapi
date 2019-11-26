namespace GoGoApi.Models
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
    }
}
