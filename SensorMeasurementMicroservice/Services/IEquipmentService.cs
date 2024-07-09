using SensorMeasurementMicroservice.Models;

namespace SensorMeasurementMicroservice.Services
{
    public interface IEquipmentService
    {
        Task CreateEquipment(Equipment equipment);
        Task<IEnumerable<Sensor>> GetSensorsWithMeasurements(string equipmentId);
    }
}
