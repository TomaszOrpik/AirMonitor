using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratoria.Models
{
    public class MeasurmentsEntity
    {
        public MeasurmentsEntity()
        {

        }
        public MeasurmentsEntity(Measurements measurement)
        {
            this.CurrentDisplayValue = measurement.CurrentDisplayValue;
            this.Current = measurement.Current.Id;
            this.History = JsonConvert.SerializeObject(measurement.History);
            this.Forecast = JsonConvert.SerializeObject(measurement.Forecast);
            this.Installation = measurement.Installation.Id;

        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int CurrentDisplayValue { get; set; }
        public int Current { get; set; }
        public string History { get; set; }
        public string Forecast { get; set; }
        public string Installation { get; set; }
    }
}
