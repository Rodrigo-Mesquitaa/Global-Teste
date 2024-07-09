using Microsoft.AspNetCore.Mvc;
using SensorMeasurementMicroservice.Models;
using SensorMeasurementMicroservice.Services;

namespace SensorMeasurementMicroservice.Controllers
{
    [ApiController]
    [Route("api/sensor")]
    public class SensorController : ControllerBase
    {
        private readonly ISensorService _sensorService;

        public SensorController(ISensorService sensorService)
        {
            _sensorService = sensorService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSensor([FromBody] Sensor sensor)
        {
            await _sensorService.CreateSensor(sensor);
            return Ok();
        }
    }
}
