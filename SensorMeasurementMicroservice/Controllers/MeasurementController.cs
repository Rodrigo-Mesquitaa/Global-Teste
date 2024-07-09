using Microsoft.AspNetCore.Mvc;
using SensorMeasurementMicroservice.Models;
using SensorMeasurementMicroservice.Services;


namespace SensorMeasurementMicroservice.Controllers
{
    [ApiController]
    [Route("api/measurement")]
    public class MeasurementController : ControllerBase
    {
        private readonly IMeasurementService _measurementService;

        public MeasurementController(IMeasurementService measurementService)
        {
            _measurementService = measurementService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMeasurement([FromBody] Measurement measurement, [FromQuery] string sensorId)
        {
            if (measurement == null || string.IsNullOrEmpty(sensorId))
            {
                return BadRequest("Measurement or sensorId cannot be null or empty.");
            }

            measurement.SensorId = sensorId;
            measurement.Timestamp = DateTime.UtcNow;

            await _measurementService.AddMeasurement(measurement);

            return Ok();
        }
    }
}
