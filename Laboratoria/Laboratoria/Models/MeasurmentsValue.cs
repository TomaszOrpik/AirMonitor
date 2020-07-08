using SQLite;

namespace Laboratoria.Models
{
    public class MeasurementValue
    {
        public MeasurementValue()
        {

        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Value { get; set; }
    }
}
