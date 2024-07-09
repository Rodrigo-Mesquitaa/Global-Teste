using MongoDB.Driver;
using Moq;
using SensorMeasurementMicroservice.Models;
using SensorMeasurementMicroservice.Services;
using Xunit;

namespace SensorMeasurementMicroservice.Tests
{
    public class EquipmentServiceTests
    {
        [Fact]
        public async Task CreateEquipment_ValidEquipment_ShouldInsertSuccessfully()
        {
            // Arrange
            var mockCollection = new Mock<IMongoCollection<Equipment>>();
            var mockSensorCollection = new Mock<IMongoCollection<Sensor>>();
            var mockMeasurementCollection = new Mock<IMongoCollection<Measurement>>();
            var mockDatabase = new Mock<IMongoDatabase>();
            mockDatabase.Setup(db => db.GetCollection<Equipment>(It.IsAny<string>(), null))
                        .Returns(mockCollection.Object);
            mockDatabase.Setup(db => db.GetCollection<Sensor>(It.IsAny<string>(), null))
                        .Returns(mockSensorCollection.Object);
            mockDatabase.Setup(db => db.GetCollection<Measurement>(It.IsAny<string>(), null))
                        .Returns(mockMeasurementCollection.Object);

            var equipmentService = new EquipmentService(mockDatabase.Object);

            var equipment = new Equipment
            {
                Name = "Equipment A"
            };

            // Act
            await equipmentService.CreateEquipment(equipment);

            // Assert
            mockCollection.Verify(
                collection => collection.InsertOneAsync(
                    It.IsAny<Equipment>(),
                    null,
                    default),
                Times.Once);
        }
    }
}
