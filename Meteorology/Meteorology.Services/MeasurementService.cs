using Meteorology.Infrastructure.Interfaces;
using Meteorology.Models;
using Meteorology.Models.EntityHelper;
using Meteorology.Models.Enums;
using Meteorology.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meteorology.Services
{
    /// <summary>
    /// Service to get Measurement of the Sensors
    /// </summary>
    public class MeasurementService : IMeasurementService
    {
        /// <summary>
        /// Blob Storage Repository
        /// </summary>
        private readonly IBlobStorage blobStorage;

        /// <summary>
        /// Constructor to initialize variables
        /// </summary>
        /// <param name="blobStorage">blobRepository</param>
        public MeasurementService(IBlobStorage blobStorage)
        {
            this.blobStorage = blobStorage;
        }

        /// <summary>
        /// Collect all data points for one unit and one day.
        /// </summary>
        /// <param name="deviceId">device Id</param>
        /// <param name="date">Date</param>
        /// <returns>List of Sensor Data</returns>
        public async Task<IEnumerable<SensorData>> GetDeviceDataAsync(string deviceId, DateTime date)
        {
            try
            {
                var response = new List<SensorData>();
                foreach (var sensor in Enum.GetNames(typeof(Sensors)))
                {
                    var result = await blobStorage.GetContentAsync($"{deviceId}/{sensor.ToLower()}/{date.ToString("yyyy-MM-dd")}.csv");
                    var values = EntityHelper.GetValue(result);
                    response.Add(new SensorData { Sensor = sensor, Values = values });
                }
                return response;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Collect all of the measurements for one day, one sensor type, and one unit.
        /// </summary>
        /// <param name="deviceId">Device Id</param>
        /// <param name="date">Date</param>
        /// <param name="sensor">Sensor</param>
        /// <returns>Returns Sensor Data</returns>
        public async Task<SensorData> GetSensorDataAsync(string deviceId, DateTime date, Sensors sensor)
        {
            try
            {
                var result = await blobStorage.GetContentAsync($"{deviceId}/{sensor.ToString().ToLower()}/{date.ToString("yyyy-MM-dd")}.csv");
                var values = EntityHelper.GetValue(result);
                return new SensorData { Sensor = sensor.ToString(), Values = values };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
