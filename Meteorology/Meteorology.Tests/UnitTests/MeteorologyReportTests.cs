using Meteorology.Infrastructure.Interfaces;
using Meteorology.Models;
using Meteorology.Models.Enums;
using Meteorology.Services;
using Meteorology.Services.Interfaces;
using MeteorologyReport.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Meteorology.Tests.UnitTests
{
    /// <summary>
    /// Unit Tests
    /// </summary>
    public class MeteorologyReportTests
    {
        /// <summary>
        /// variable for controller.
        /// </summary>
        public DevicesController controller;
        /// <summary>
        /// Service Mock object
        /// </summary>
        public Mock<IMeasurementService> serviceMock;

        /// <summary>
        /// service object
        /// </summary>
        public MeasurementService service;
        /// <summary>
        /// repository Mock object.
        /// </summary>
        public Mock<IBlobStorage> repoMock;

        /// <summary>
        /// Constructor to initialize variables
        /// </summary>
        public MeteorologyReportTests()
        {
            serviceMock = new Mock<IMeasurementService>();
            controller = new DevicesController(serviceMock.Object);

            repoMock = new Mock<IBlobStorage>();
            service = new MeasurementService(repoMock.Object);
        }


        //Controller Tests
        /// <summary>
        /// test method to Test the GetData Action of Controller
        /// </summary>
        /// <returns>task</returns>
        [Fact]
        public async Task DevicesController_GetData_Tests()
        {
            //Valid Tests
            serviceMock.Setup(t=>t.GetSensorDataAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<Sensors>())).ReturnsAsync(new SensorData { Sensor = Sensors.Humidity.ToString()});
            var result = await controller.GetData("deviceId", DateTime.Now, Sensors.Humidity);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var sensorData = Assert.IsType<SensorData>(actionResult.Value);
            Assert.Equal(sensorData.Sensor, Sensors.Humidity.ToString());

            //Invalid Tests
            result = await controller.GetData("deviceId", DateTime.Now.AddDays(1), Sensors.Humidity);
            Assert.IsType<BadRequestObjectResult>(result);
        }

        /// <summary>
        /// Method to test GetDataForDevice Action of controller.
        /// </summary>
        /// <returns>task</returns>
        [Fact]
        public async Task DevicesController_GetDataForDevice_Tests()
        {
            //Valid Tests
            serviceMock.Setup(t => t.GetDeviceDataAsync(It.IsAny<string>(), It.IsAny<DateTime>())).ReturnsAsync(new List<SensorData>());
            var result = await controller.GetDataForDevice("deviceId", DateTime.Now);
            var actionResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<SensorData>>(actionResult.Value);

            //Invalid Tests
            result = await controller.GetDataForDevice("deviceId", DateTime.Now.AddDays(1));
            Assert.IsType<BadRequestObjectResult>(result);
        }

        //Service Tests
        /// <summary>
        /// Method to test GetDeviceDataAsync method of services
        /// </summary>
        /// <returns>task</returns>
        [Fact]
        public async Task MeasurementService_GetDeviceDataAsync_Tests()
        {
            //Valid Tests
            repoMock.Setup(t=>t.GetContentAsync(It.IsAny<string>())).ReturnsAsync("2018-11-29T07:54:55,,03");
            var result = await service.GetDeviceDataAsync("deviceId", DateTime.Now);
            Assert.IsType<List<SensorData>>(result);

            //Invalid Tests
            repoMock.Setup(t => t.GetContentAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            result = await service.GetDeviceDataAsync("deviceId", DateTime.Now);
            Assert.Null(result);
        }

        /// <summary>
        /// method to test GetSensorDataAsync method of services
        /// </summary>
        /// <returns>task</returns>
        [Fact]
        public async Task MeasurementService_GetSensorDataAsync_Tests()
        {
            //Valid Tests
            repoMock.Setup(t => t.GetContentAsync(It.IsAny<string>())).ReturnsAsync("2018-11-29T07:54:55,,03");
            var result = await service.GetSensorDataAsync("deviceId", DateTime.Now, Sensors.Humidity);
            Assert.IsType<SensorData>(result);
            Assert.Equal(result.Sensor, Sensors.Humidity.ToString());

            //Invalid Tests
            repoMock.Setup(t => t.GetContentAsync(It.IsAny<string>())).ThrowsAsync(new Exception());
            result = await service.GetSensorDataAsync("deviceId", DateTime.Now, Sensors.Humidity);
            Assert.Null(result);
        }
    }
}
