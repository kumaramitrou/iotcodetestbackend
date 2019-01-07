using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MeteorologyReport
{
    /// <summary>
    /// Starting point for Application
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Starting Method for Application
        /// </summary>
        /// <param name="args">env args</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Method to initialize Webhost
        /// </summary>
        /// <param name="args">Env Args</param>
        /// <returns>host builder</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
