using Infrastructure.Mailing;

namespace Infrastructure
{
    public class InfrastructureSettings
    {
        public string DbConnectionString { get; set; }

        public SendGridSettings SendGridSettings { get; set; }
    }
}