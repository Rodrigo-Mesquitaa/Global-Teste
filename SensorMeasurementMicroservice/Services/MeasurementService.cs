using MongoDB.Driver;
using SensorMeasurementMicroservice.Models;

namespace SensorMeasurementMicroservice.Services
{
    public class MeasurementService : IMeasurementService
    {
        private readonly IMongoCollection<Measurement> _measurementCollection;
        private readonly ISensorService _sensorService; // Serviço de sensores
        private IMongoDatabase @object;

        public MeasurementService(IMongoDatabase @object)
        {
            this.@object = @object;
        }

        public MeasurementService(IMongoDatabase database, ISensorService sensorService)
        {
            _measurementCollection = database.GetCollection<Measurement>("measurements");
            _sensorService = sensorService;
        }

        public async Task AddMeasurement(Measurement measurement)
        {
            await _measurementCollection.InsertOneAsync(measurement);
        }

        public IEnumerable<Sensor> GetAllSensors()
        {
            // Supondo que ISensorService tenha um método para obter todos os sensores
            return _sensorService.GetAllSensors();
        }

        public IEnumerable<Measurement> GetLastMeasurements(string sensorId, int limit)
        {
            // Consulta para obter os últimos registros de medições para um sensor específico
            var filter = Builders<Measurement>.Filter.Eq(m => m.SensorId, sensorId);
            var sort = Builders<Measurement>.Sort.Descending(m => m.Timestamp);
            var lastMeasurements = _measurementCollection.Find(filter).Sort(sort).Limit(limit).ToList();
            return lastMeasurements;
        }

        public async Task<IEnumerable<Measurement>> GetRecentMeasurementsAsync(int limit)
        {
            // Consulta para obter as medições mais recentes ordenadas pela data de criação
            var sort = Builders<Measurement>.Sort.Descending(m => m.Timestamp);
            var recentMeasurements = await _measurementCollection.Find(Builders<Measurement>.Filter.Empty)
                .Sort(sort)
                .Limit(limit)
                .ToListAsync();
            return recentMeasurements;
        }

        Task IMeasurementService.AddMeasurement(Measurement measurement)
        {
            throw new NotImplementedException();
        }

        IEnumerable<object> IMeasurementService.GetAllSensors()
        {
            throw new NotImplementedException();
        }

        object IMeasurementService.GetLastMeasurements(object id, int v)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<object>> IMeasurementService.GetRecentMeasurementsAsync(int limit)
        {
            throw new NotImplementedException();
        }
    }
}
