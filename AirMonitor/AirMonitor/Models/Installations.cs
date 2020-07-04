using Newtonsoft.Json;
using Xamarin.Essentials;

namespace AirMonitor.Models
{
    public class Installations
    {
        public string Id { get; set; }
        public Location Location { get; set; }
        public Address Address { get; set; }
        public double Elevation { get; set; }
        [JsonProperty(PropertyName = "airly")]
        public bool IsAirlyInstallation { get; set; }
    }
}
