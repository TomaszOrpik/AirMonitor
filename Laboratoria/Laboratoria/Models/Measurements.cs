using Newtonsoft.Json;

namespace Laboratoria.Models
{
    public class Measurements
    {
        public Measurements()
        {

        }
        public Measurements(MeasurmentsEntity measurmentsEntity)
        {
                this.CurrentDisplayValue = measurmentsEntity.CurrentDisplayValue;
                this.History = JsonConvert.DeserializeObject<MeasurementItem[]>(measurmentsEntity.History);
                this.Forecast = JsonConvert.DeserializeObject<MeasurementItem[]>(measurmentsEntity.Forecast);
        }
        public int CurrentDisplayValue { get; set; }
        public MeasurementItem Current { get; set; }
        public MeasurementItem[] History { get; set; }
        public MeasurementItem[] Forecast { get; set; }
        public Installations Installation { get; set; }
    }
}