using System.Reflection;
using System.Text;
using Application.Services;
using Application.Settings;
using Application.Utils.Security;
using Domain.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            ApplicationSettings applicationSettings)
        {
            services.AddSingleton(applicationSettings);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IAccountService, AccountService>();
            services.AddJwtAuthentication(applicationSettings.AccessToken);
            services.AddFluentValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            return services;
        }

        private static void AddJwtAuthentication(this IServiceCollection services, AccessToken accessTokenSettings)
        {
            services.AddSingleton<ISecurityTokenService, SecurityTokenService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenSettings.Key)),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });
        }
    }
}