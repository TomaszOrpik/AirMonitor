using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laboratoria.Models
{
    public class MeasurmentItemEntity
    {
        public MeasurmentItemEntity()
        {

        }
        public MeasurmentItemEntity(MeasurementItem measurementItem)
        {
            this.FromDateTime = measurementItem.FromDateTime;
            this.TillDateTime = measurementItem.TillDateTime;
            this.Values = JsonConvert.SerializeObject(measurementItem.Values);
            this.Indexes = JsonConvert.SerializeObject(measurementItem.Indexes);
            this.Standards = JsonConvert.SerializeObject(measurementItem.Standards);
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime TillDateTime { get; set; }
        public string Values { get; set; }
        public string Indexes { get; set; }
        public string Standards { get; set; }
    }
}
