using System.Reflection;
using System.Text;
using Application.Services;
using Application.Utils.Email;
using Application.Utils.Email.Templates;
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
            services.AddJwtAuthentication(applicationSettings.AccessTokenSettings);
            services.AddFluentValidation(configuration => configuration.LocalizationEnabled = false);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<VerifyEmailAddressEmailTemplate>();
            services.AddSingleton<RequestResetPasswordTemplate>();
            services.AddSingleton<ChangedPasswordTemplate>();

            return services;
        }

        private static void AddJwtAuthentication(this IServiceCollection services,
            AccessTokenSettings accessTokenSettingsSettings)
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
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(accessTokenSettingsSettings.Key)),
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });
        }
    }
}