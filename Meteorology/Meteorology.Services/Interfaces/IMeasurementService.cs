using Meteorology.Models;
using Meteorology.Models.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meteorology.Services.Interfaces
{
    /// <summary>
    /// Interface for Measurement Service
    /// </summary>
    public interface IMeasurementService
    {
        /// <summary>
        /// Collect all of the measurements for one day, one sensor type, and one unit.
        /// </summary>
        /// <param name="deviceId">device Id</param>
        /// <param name="date">date</param>
        /// <param name="sensor">sensorName</param>
        /// <returns></returns>
        Task<SensorData> GetSensorDataAsync(string deviceId, DateTime date, Sensors sensor);

        /// <summary>
        /// Collect all data points for one unit and one day.
        /// </summary>
        /// <param name="deviceId">deviceId</param>
        /// <param name="date">date</param>
        /// <returns>sensorData</returns>
        Task<IEnumerable<SensorData>> GetDeviceDataAsync(string deviceId, DateTime date);
    }
}
