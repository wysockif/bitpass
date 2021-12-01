using System;
using Application.InfrastructureInterfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, InfrastructureSettings settings)
        {
            services.AddSingleton(settings);
            services.AddDatabase(settings.DbConnectionString);
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }

        private static IServiceCollection AddDatabase(this IServiceCollection services, string dbConnectionString)
        {
            // Action<DbContextOptionsBuilder> action = options =>
            // {
            //     options.UseNpgsql(dbConnectionString);
            // };

            services.AddDbContext<IStorage, DatabaseContext>(options => options.UseNpgsql(dbConnectionString));
            return services;
        }
    }
}