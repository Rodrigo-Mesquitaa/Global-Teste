using MongoDB.Driver;

namespace SensorMeasurementMicroservice.Infrastructure
{
    public static class MongoDbService
    {
        public static IMongoDatabase Configure(IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration.GetConnectionString("MongoDb"));
            return mongoClient.GetDatabase("sensorDb"); // Nome do banco de dados
        }
    }
}
