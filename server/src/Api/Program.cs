using System;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<IStorage>();
                db.Database.Migrate();
                Console.WriteLine("Migrating latest...");
            }

            host.Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    if (Environment.GetEnvironmentVariable("PORT") != default)
                    {
                        webBuilder.UseUrls($"http://0.0.0.0:{Environment.GetEnvironmentVariable("PORT")}");
                    }

                    webBuilder.UseKestrel(opt => opt.AddServerHeader = false);
                    webBuilder.UseStartup<Startup>();
                });
    }
}