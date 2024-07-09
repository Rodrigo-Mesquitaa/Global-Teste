using SensorMeasurementMicroservice.Models;

namespace SensorMeasurementMicroservice.Services
{
    public interface ISensorService
    {
        Task CreateSensor(Sensor sensor);
        IEnumerable<Sensor> GetAllSensors();
    }
}
