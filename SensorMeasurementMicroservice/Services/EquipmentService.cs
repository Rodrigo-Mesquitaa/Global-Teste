using MongoDB.Driver;
using SensorMeasurementMicroservice.Models;

namespace SensorMeasurementMicroservice.Services
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IMongoCollection<Equipment> _equipmentCollection;
        private readonly IMongoCollection<Sensor> _sensorCollection;
        private readonly IMongoCollection<Measurement> _measurementCollection;

        public EquipmentService(IMongoDatabase database)
        {
            _equipmentCollection = database.GetCollection<Equipment>("equipments");
            _sensorCollection = database.GetCollection<Sensor>("sensors");
            _measurementCollection = database.GetCollection<Measurement>("measurements");
        }

        public async Task CreateEquipment(Equipment equipment)
        {
            await _equipmentCollection.InsertOneAsync(equipment);
        }

        public async Task<IEnumerable<Sensor>> GetSensorsWithMeasurements(string equipmentId)
        {
            var equipment = await _equipmentCollection.Find(e => e.Id == equipmentId).FirstOrDefaultAsync();
            if (equipment == null)
            {
                return null;
            }

            var sensors = await _sensorCollection.Find(s => s.EquipmentId == equipmentId).ToListAsync();
            foreach (var sensor in sensors)
            {
                var measurements = await _measurementCollection.Find(m => m.SensorId == sensor.Id)
                                                                .SortByDescending(m => m.Timestamp)
                                                                .Limit(10)
                                                                .ToListAsync();
                sensor.Measurements = measurements;
            }

            return sensors;
        }
    }
}
