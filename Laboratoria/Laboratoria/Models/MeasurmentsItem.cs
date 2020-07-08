using Newtonsoft.Json;
using SQLite;
using System;

namespace Laboratoria.Models
{
    public class MeasurementItem
    {
        public MeasurementItem()
        {

        }
        public MeasurementItem(MeasurmentItemEntity measurementItemEntity)
        {
            this.Id = measurementItemEntity.Id;
            this.FromDateTime = measurementItemEntity.FromDateTime;
            this.TillDateTime = measurementItemEntity.TillDateTime;
            this.TillDateTime = measurementItemEntity.TillDateTime;
            this.Values = JsonConvert.DeserializeObject<MeasurementValue[]>(measurementItemEntity.Values);
            this.Indexes = JsonConvert.DeserializeObject<AirQualityIndex[]>(measurementItemEntity.Indexes);
            this.Standards = JsonConvert.DeserializeObject<AirQualityStandard[]>(measurementItemEntity.Standards);
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime TillDateTime { get; set; }
        public MeasurementValue[] Values { get; set; }
        public AirQualityIndex[] Indexes { get; set; }
        public AirQualityStandard[] Standards { get; set; }
    }
}