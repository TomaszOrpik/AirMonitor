using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Laboratoria.Models
{
    public class InstallationsEntity
    {
        public InstallationsEntity()
        {

        }
        public InstallationsEntity(Installations installations)
        {
            this.Id = installations.Id;
            this.Location = JsonConvert.SerializeObject(installations.Location);
            this.Address = JsonConvert.SerializeObject(installations.Address);
            this.Elevation = installations.Elevation;
            this.IsAirlyInstallation = installations.IsAirlyInstallation;
        }
        [PrimaryKey]
        public string Id { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public double Elevation { get; set; }
        [JsonProperty(PropertyName = "airly")]
        public bool IsAirlyInstallation { get; set; }
        }
    }
