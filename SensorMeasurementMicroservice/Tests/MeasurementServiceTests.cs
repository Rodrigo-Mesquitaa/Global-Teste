using MongoDB.Driver;
using Moq;
using SensorMeasurementMicroservice.Models;
using SensorMeasurementMicroservice.Services;
using Xunit;

namespace SensorMeasurementMicroservice.Tests
{
    public class MeasurementServiceTests
    {
        [Fact]
        public async Task AddMeasurement_ValidMeasurement_ShouldInsertSuccessfully()
        {
            // Arrange
            var mockCollection = new Mock<IMongoCollection<Measurement>>();
            var mockDatabase = new Mock<IMongoDatabase>();
            mockDatabase.Setup(db => db.GetCollection<Measurement>(It.IsAny<string>(), null))
                        .Returns(mockCollection.Object);

            var measurementService = new MeasurementService(mockDatabase.Object);

            var measurement = new Measurement
            {
                SensorId = "Sensor1",
                Value = 25.5,
                Timestamp = DateTime.UtcNow
            };

            // Act
            await measurementService.AddMeasurement(measurement);

            // Assert
            mockCollection.Verify(
                collection => collection.InsertOneAsync(
                    It.IsAny<Measurement>(),
                    null,
                    default),
                Times.Once);
        }
    }
}
