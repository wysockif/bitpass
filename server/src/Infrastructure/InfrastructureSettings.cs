#pragma warning disable 8618

using System;
using Infrastructure.Mailing;
using Npgsql;

#pragma warning disable 8603

namespace Infrastructure
{
    public class InfrastructureSettings
    {
        private string _dbConnectionString;

        public SendGridSettings SendGridSettings { get; set; }

        public string DbConnectionString
        {
            get
            {
                var databaseUrl = Environment.GetEnvironmentVariable(_dbConnectionString ?? "");
                return databaseUrl != null ? ParseDatabaseUrl(databaseUrl) : _dbConnectionString;
            }

            set => _dbConnectionString = value;
        }

        private string ParseDatabaseUrl(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                Pooling = true,
                SslMode = SslMode.Require,
                TrustServerCertificate = true,
            };

            return builder.ToString();
        }
    }
}