using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using SensorMeasurementMicroservice.Models;
using SensorMeasurementMicroservice.Services;

namespace SensorMeasurementMicroservice.Controllers
{
    [ApiController]
    [Route("api/equipment")]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEquipment([FromBody] Equipment equipment)
        {
            await _equipmentService.CreateEquipment(equipment);
            return Ok();
        }

        [HttpGet("{equipmentId}/sensors")]
        public async Task<IActionResult> GetSensorsWithMeasurements(string equipmentId)
        {
            var sensors = await _equipmentService.GetSensorsWithMeasurements(equipmentId);

            if (sensors == null || !sensors.Any())
            {
                return NotFound();
            }

            return Ok(sensors);
        }
    }
}
