using System.Reflection;
using Application.Services;
using Domain.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, ApplicationSettings applicationSettings)
        {
            services.AddSingleton(applicationSettings);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
    }
}