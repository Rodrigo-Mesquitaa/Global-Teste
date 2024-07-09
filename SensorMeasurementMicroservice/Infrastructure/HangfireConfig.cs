using Hangfire;
using Hangfire.Mongo;
using SensorMeasurementMicroservice.Jobs;
using SensorMeasurementMicroservice.Services;

namespace SensorMeasurementMicroservice.Configurations
{
    public static class HangfireConfig
    {
        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do Hangfire
            services.AddHangfire(config =>
            {
                config.UseFilter(new HangfireAuthorizationFilter()); // Substitua pelo filtro de autorização real
                config.UseMongoStorage(configuration.GetConnectionString("MongoDb"), "hangfire");
            });

            // Serviço de background - Hangfire Job
            services.AddSingleton<IMeasurementJobService, MeasurementJobService>();

            // Adiciona o Hangfire Server para executar os jobs
            services.AddHangfireServer();
        }

        public static void StartHangfireJobs(IServiceProvider serviceProvider)
        {
            // Inicia o serviço de background do Hangfire Job
            var jobService = serviceProvider.GetRequiredService<IMeasurementJobService>();
            jobService.ProcessMeasurementsJob();
        }
    }
}
