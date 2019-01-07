using Meteorology.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace MeteorologyReport
{
    /// <summary>
    /// Start up class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor to initialize variables
        /// </summary>
        /// <param name="configuration">configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Method to resolve dependencies
        /// </summary>
        /// <param name="services">service collection</param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var swaggerInfo = new Info()
            {
                Title = Constants.ApplicationTitle,
                Version = Constants.ApplicationVersion
            };

            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(string.Format(@"{0}\MeteorologyReport.xml",
                           AppDomain.CurrentDomain.BaseDirectory), true);
                c.DescribeAllEnumsAsStrings();
            });

            services.AddLogging();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(swaggerInfo.Version, swaggerInfo);
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            DependencyResolver.Register(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        /// <summary>
        /// Confitures the Environment
        /// </summary>
        /// <param name="app">applicationBuilder</param>
        /// <param name="env">hostingEnvironment</param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            builder.AddJsonFile("appsettings.json", false, true);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStatusCodePages(async context =>
            {
                context.HttpContext.Response.ContentType = "text/plain";
                await context.HttpContext.Response.WriteAsync("Please contact Administrator. The api you are looking for is under maintenance or not available. Status Code:" + context.HttpContext.Response.StatusCode);
            });

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Constants.ApplicationTitle}{Constants.ApplicationVersion}");
            });
        }
    }
}
