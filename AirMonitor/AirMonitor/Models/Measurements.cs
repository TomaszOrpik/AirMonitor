namespace AirMonitor.Models
{
    public class Measurements
    {
        public int CurrentDisplayValue { get; set; }
        public MeasurementItem Current { get; set; }
        public MeasurementItem[] History { get; set; }
        public MeasurementItem[] Forecast { get; set; }
        public Installations Installation { get; set; }
    }
}