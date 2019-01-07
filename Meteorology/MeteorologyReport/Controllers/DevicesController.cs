using Meteorology.Models.Enums;
using Meteorology.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MeteorologyReport.Controllers
{
    /// <summary>
    /// Controller to Fetch the details
    /// </summary>
    [Route("api/v{version}/devices")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        /// <summary>
        /// Variable for measurementService.
        /// </summary>
        private readonly IMeasurementService measurementService;

        /// <summary>
        /// Constructor to initialize variables
        /// </summary>
        /// <param name="measurementService">measurementService</param>
        public DevicesController(IMeasurementService measurementService)
        {
            this.measurementService = measurementService;
        }

        /// <summary>
        /// Collect all of the measurements for one day, one sensor type, and one unit.
        /// </summary>
        /// <returns>Ok Result</returns>
        [HttpGet]
        [Route("getdata/{deviceId}/{date}/{sensor}")]
        public async Task<IActionResult> GetData(string deviceId, DateTime date, Sensors sensor)
        {
            if (date >= DateTime.Now)
            {
                return BadRequest("Future dates are not allowed.");
            }
            return Ok(await measurementService.GetSensorDataAsync(deviceId, date, sensor));
        }

        /// <summary>
        /// Collect all data points for one unit and one day.
        /// </summary>
        /// <returns>Ok Result</returns>
        [HttpGet]
        [Route("getdatafordevice/{deviceId}/{date}")]
        public async Task<IActionResult> GetDataForDevice(string deviceId, DateTime date)
        {
            if (date >= DateTime.Now)
            {
                return BadRequest("Future dates are not allowed.");
            }
            return Ok(await measurementService.GetDeviceDataAsync(deviceId, date));
        }
    }
}
