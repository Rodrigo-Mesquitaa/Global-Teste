using Hangfire;
using Hangfire.Dashboard;
using Hangfire.Mongo;
using SensorMeasurementMicroservice.Infrastructure;
using SensorMeasurementMicroservice.Jobs;
using SensorMeasurementMicroservice.Services;

namespace SensorMeasurementMicroservice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Configuração do MongoDB (exemplo de configuração, ajuste conforme sua implementação)
            services.AddSingleton(provider => MongoDbService.Configure(Configuration));

            // Configuração do RabbitMQ (exemplo de configuração, ajuste conforme sua implementação)
            RabbitMQService.Configure(services);

            // Serviços da aplicação
            services.AddScoped<IMeasurementService, MeasurementService>();
            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<IEquipmentService, EquipmentService>();

            // Configuração do Hangfire
            services.AddHangfire(config =>
            {
                config.UseFilter(new HangfireAuthorizationFilter()); // Substitua pelo filtro de autorização real
                config.UseMongoStorage(Configuration.GetConnectionString("MongoDb"), "hangfire");
            });

            // Serviço de background - Hangfire Job
            services.AddHangfireServer();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Configuração do Hangfire Dashboard (opcional)
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() } // Substitua pelo filtro de autorização real
            });

            // Iniciar o serviço de background do Hangfire Job
            var jobService = app.ApplicationServices.GetRequiredService<IMeasurementJobService>();
            jobService.ProcessMeasurementsJob();
        }
    }

    // Filtro de autorização do Hangfire (exemplo básico)
    internal class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // Aqui você deve implementar a lógica de autorização desejada para o Dashboard do Hangfire
            // Por exemplo, verificar se o usuário tem permissão para acessar o Dashboard
            // Este é um exemplo básico apenas para ilustração
            return true; // Permitir acesso sem autenticação para fins de demonstração
        }
    }
}
