using MongoDB.Driver;
using SensorMeasurementMicroservice.Models;

namespace SensorMeasurementMicroservice.Services
{
    public class SensorService : ISensorService
    {
        private readonly IMongoCollection<Sensor> _sensorCollection;

        public SensorService(IMongoDatabase database)
        {
            _sensorCollection = database.GetCollection<Sensor>("sensors");
        }

        public async Task CreateSensor(Sensor sensor)
        {
            await _sensorCollection.InsertOneAsync(sensor);
        }

        public IEnumerable<Sensor> GetAllSensors()
        {
            throw new NotImplementedException();
        }
    }
}
