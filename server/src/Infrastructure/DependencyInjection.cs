using Application.InfrastructureInterfaces;
using Infrastructure.Mailing;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            InfrastructureSettings settings)
        {
            services.AddSingleton(settings);
            services.AddSingleton(settings.SendGridSettings);
            services.AddDbContext<IStorage, DatabaseContext>(options => options.UseNpgsql(settings.DbConnectionString));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<IEmailGateway, SendGridEmailGateway>();
            services
                .AddFluentEmail(settings.SendGridSettings.FromName)
                .AddSendGridSender(settings.SendGridSettings.ApiKey);

            return services;
        }
    }
}