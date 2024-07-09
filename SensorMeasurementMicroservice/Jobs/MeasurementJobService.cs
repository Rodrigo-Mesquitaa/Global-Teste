using Hangfire;
using SensorMeasurementMicroservice.Services;


namespace SensorMeasurementMicroservice.Jobs
{
    public class MeasurementJobService : IMeasurementJobService
    {
        private readonly ILogger<MeasurementJobService> _logger;
        private readonly IMeasurementService _measurementService;

        public MeasurementJobService(ILogger<MeasurementJobService> logger, IMeasurementService measurementService)
        {
            _logger = logger;
            _measurementService = measurementService;
        }

        public async Task ProcessMeasurementsJob()
        {
            _logger.LogInformation("Starting ProcessMeasurementsJob at {time}", DateTime.UtcNow);

            try
            {
                var jobId = BackgroundJob.Enqueue(() => ProcessRecentMeasurements(50));
                _logger.LogInformation($"Job {jobId} queued.");

                _logger.LogInformation("ProcessMeasurementsJob completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in ProcessMeasurementsJob: {Message}", ex.Message);
                throw;
            }
        }

        public async Task ProcessRecentMeasurements(int limit)
        {
            try
            {
                _logger.LogInformation($"Processing recent {limit} measurements...");

                // Obter os últimos registros de medições do serviço
                var recentMeasurements = await _measurementService.GetRecentMeasurementsAsync(limit);

                foreach (var measurement in recentMeasurements)
                {
                    // Aqui você pode implementar a lógica específica para processar cada medição
                    _logger.LogInformation($"Processing measurement: {measurement}");
                    // Exemplo: _measurementService.ProcessMeasurement(measurement);
                }

                _logger.LogInformation($"Processed {Convert.ToString(recentMeasurements)} measurements successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing recent measurements: {Message}", ex.Message);
                throw;
            }
        }

    }
}