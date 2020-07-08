using Newtonsoft.Json;
using Xamarin.Essentials;

namespace Laboratoria.Models
{
    public class Installations
    {
        public Installations()
        {

        }
        public Installations(InstallationsEntity installationsEntity)
        {
            this.Id = installationsEntity.Id;
            this.Location = JsonConvert.DeserializeObject<Location>(installationsEntity.Location);
            this.Address = JsonConvert.DeserializeObject<Address>(installationsEntity.Address);
            this.Elevation = installationsEntity.Elevation;
            this.IsAirlyInstallation = installationsEntity.IsAirlyInstallation;
        }
        public string Id { get; set; }
        public Location Location { get; set; }
        public Address Address { get; set; }
        public double Elevation { get; set; }
        [JsonProperty(PropertyName = "airly")]
        public bool IsAirlyInstallation { get; set; }
    }
}
