using MassTransit;
using SensorMeasurementMicroservice.Models;


namespace SensorMeasurementMicroservice.Infrastructure
{
    public static class RabbitMQService
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("measurement-queue", ep =>
                    {
                        ep.Handler<Measurement>(context =>
                        {
                            var measurement = context.Message;
                            // Lógica para processar a medição recebida do RabbitMQ
                            Console.WriteLine($"Received Measurement: {measurement}");
                            return Task.CompletedTask;
                        });
                    });
                });
            });
        }
    }
}
