using System;
using System.Collections.Generic;
using System.Text;

namespace Meteorology.Models
{
    public class SensorData
    {
        /// <summary>
        /// Sensor Type
        /// </summary>
        public string Sensor { get; set; }

        /// <summary>
        /// Reading for a particular day
        /// </summary>
        public IEnumerable<double> Values { get; set; }
    }
}
