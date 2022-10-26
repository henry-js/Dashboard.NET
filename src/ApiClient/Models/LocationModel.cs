namespace Dashboard.NET.ApiClient.Models
{
#pragma warning disable CS8618

    public class LocationModel
    {
        public string Name { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Country { get; set; }
        public LocalNames LocalNames { get; set; }
        public string State { get; set; }
    }

    public partial class LocalNames
    {
        public string Ar { get; set; }
        public string Ascii { get; set; }
        public string Bg { get; set; }
        public string De { get; set; }
        public string En { get; set; }
        public string Fa { get; set; }
        public string FeatureName { get; set; }
        public string Fi { get; set; }
        public string Fr { get; set; }
        public string He { get; set; }
        
        
        public string Ja { get; set; }
        public string Lt { get; set; }
        public string Nl { get; set; }
        public string Pl { get; set; }
        public string Pt { get; set; }
        public string Ru { get; set; }
        public string Sr { get; set; }
        public string Ca { get; set; }
    }
#pragma warning restore CS8618
}