using Api.Middlewares;
using Application;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var infrastructureSettings = new InfrastructureSettings();
            Configuration.Bind(nameof(InfrastructureSettings), infrastructureSettings);
            // infrastructureSettings.Validate();
            services.AddInfrastructure(infrastructureSettings);

            var applicationSettings = new ApplicationSettings();
            Configuration.Bind(nameof(ApplicationSettings), applicationSettings);
            services.AddApplication(applicationSettings);

            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<DeviceInfoCollectingMiddleware>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            app.UseRouting();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<DeviceInfoCollectingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}