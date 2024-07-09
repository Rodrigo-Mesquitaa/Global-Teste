using SensorMeasurementMicroservice.Models;

namespace SensorMeasurementMicroservice.Services
{
    public interface IMeasurementService
    {
        Task AddMeasurement(Measurement measurement);
        IEnumerable<object> GetAllSensors();
        object GetLastMeasurements(object id, int v);
        Task<IEnumerable<object>> GetRecentMeasurementsAsync(int limit);
    }
}
