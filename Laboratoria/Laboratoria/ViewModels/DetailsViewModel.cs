using Laboratoria.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Laboratoria.ViewModels
{
    class DetailsViewModel : BaseViewModel
    {
        public DetailsViewModel()
        {
        }

        private Measurements _item;
        public Measurements Item
        {
            get => _item;
            set
            {
                SetProperty(ref _item, value);

                UpdateProperties();
            }
        }

        private void UpdateProperties()
        {
            if (Item?.Current == null) return;
            var current = Item?.Current;
            var index = current.Indexes?.FirstOrDefault(c => c.Name == "AIRLY_CAQI");
            var values = current.Values;
            var standards = current.Standards;

            CAQI = (int)Math.Round(index?.Value ?? 0);
            airQuality = index.Description;
            description = index.Advice;
            pm25Value = (int)Math.Round(values?.FirstOrDefault(s => s.Name == "PM25")?.Value ?? 0);
            pm10Value = (int)Math.Round(values?.FirstOrDefault(s => s.Name == "PM10")?.Value ?? 0);
            humidityValue = (int)Math.Round(values?.FirstOrDefault(s => s.Name == "HUMIDITY")?.Value ?? 0);
            pressureValue = (int)Math.Round(values?.FirstOrDefault(s => s.Name == "PRESSURE")?.Value ?? 0);
            pm25Percent = (int)Math.Round(standards?.FirstOrDefault(s => s.Pollutant == "PM25")?.Percent ?? 0);
            pm10Percent = (int)Math.Round(standards?.FirstOrDefault(s => s.Pollutant == "PM10")?.Percent ?? 0);
        }

        private int _CAQI;
        public int CAQI
        {
            get => _CAQI;
            set => SetProperty(ref _CAQI, value);
        }
        private string _airQuality;
        public string airQuality { get => _airQuality;
            set => SetProperty(ref _airQuality, value);
        }
        private string _description;
        public string description {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private int _pm25Value;
        public int pm25Value { get => _pm25Value;
            set => SetProperty(ref _pm25Value, value);
        }
        private int _pm25Percent;
        public int pm25Percent { get => _pm25Percent;
            set => SetProperty(ref _pm25Percent, value);
        }
        private int _pm10Value;
        public int pm10Value { get => _pm10Value;
            set => SetProperty(ref _pm10Value, value);
        }
        private int _pm10Percent;
        public int pm10Percent { get => _pm10Percent;
            set => SetProperty(ref _pm10Percent, value); 
        }
        private double _humidityValue;
        public double humidityValue { get => _humidityValue;
            set => SetProperty(ref _humidityValue, value);
        }
        private int _pressureValue;
        public int pressureValue
        {
            get => _pressureValue;
            set => SetProperty(ref _pressureValue, value);
        }
    }
}
