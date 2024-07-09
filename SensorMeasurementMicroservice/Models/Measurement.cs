using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SensorMeasurementMicroservice.Models
{
    public class Measurement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string SensorId { get; set; }
        public double Value { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
