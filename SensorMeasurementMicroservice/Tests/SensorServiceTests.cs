using MongoDB.Driver;
using Moq;
using SensorMeasurementMicroservice.Models;
using SensorMeasurementMicroservice.Services;
using Xunit;

namespace SensorMeasurementMicroservice.Tests
{
    public class SensorServiceTests
    {
        [Fact]
        public async Task CreateSensor_ValidSensor_ShouldInsertSuccessfully()
        {
            // Arrange
            var mockCollection = new Mock<IMongoCollection<Sensor>>();
            var mockDatabase = new Mock<IMongoDatabase>();
            mockDatabase.Setup(db => db.GetCollection<Sensor>(It.IsAny<string>(), null))
                        .Returns(mockCollection.Object);

            var sensorService = new SensorService(mockDatabase.Object);

            var sensor = new Sensor
            {
                Name = "Temperature Sensor",
                Location = "Room A"
            };

            // Act
            await sensorService.CreateSensor(sensor);

            // Assert
            mockCollection.Verify(
                collection => collection.InsertOneAsync(
                    It.IsAny<Sensor>(),
                    null,
                    default),
                Times.Once);
        }
    }
}
