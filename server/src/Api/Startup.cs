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

            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("FrontEndClient", builder =>
                {
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("FrontEndClient");
            app.UseRouting();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}