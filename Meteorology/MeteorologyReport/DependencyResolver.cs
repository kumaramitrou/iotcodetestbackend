using Meteorology.Infrastructure;
using Meteorology.Infrastructure.Interfaces;
using Meteorology.Services;
using Meteorology.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MeteorologyReport
{
    /// <summary>
    /// Dependency Injector
    /// </summary>
    public class DependencyResolver
    {
        /// <summary>
        /// Dependency Injection for Project
        /// </summary>
        /// <param name="services">Service Collections</param>
        public static void Register(IServiceCollection services)
        {
            //Project Dependencies
            services.AddTransient<IMeasurementService, MeasurementService>();

            services.AddTransient<IBlobStorage, BlobStorage>();
        }
    }
}
