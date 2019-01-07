using System;
using System.Collections.Generic;

namespace Meteorology.Models.EntityHelper
{
    /// <summary>
    /// Entity Helper
    /// </summary>
    public static class EntityHelper
    {
        /// <summary>
        /// Gets the value from csv file
        /// </summary>
        /// <param name="csvData">csv Data</param>
        /// <returns>list of values</returns>
        public static IEnumerable<double> GetValue(string csvData)
        {
            try
            {
                var entities = csvData.Split('\n');
                var values = new List<double>();
                foreach (var item in entities)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var entity = item.Replace("\r", "").Split(',');
                        values.Add(double.Parse(entity[2] ?? "0"));
                    }
                }
                return values;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
