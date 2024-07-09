using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SensorMeasurementMicroservice.Models
{
    public class Sensor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
        public string Location { get; set; }
        public string EquipmentId { get; internal set; }
        public List<Measurement> Measurements { get; internal set; }
    }
}
