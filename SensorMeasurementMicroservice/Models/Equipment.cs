﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SensorMeasurementMicroservice.Models
{
    public class Equipment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
