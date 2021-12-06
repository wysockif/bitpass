using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
using FluentEmail.Core;

namespace Infrastructure.Mailing
{
    public class SendGridEmailGateway : IEmailGateway
    {
        private readonly IFluentEmail _fluentEmail;
        private readonly SendGridSettings _settings;

        public SendGridEmailGateway(SendGridSettings settings, IFluentEmail fluentEmail)
        {
            _settings = settings;
            _fluentEmail = fluentEmail;
        }

        public async Task SendEmailAsync(string to, string title, string body)
        {
            _fluentEmail.SetFrom(_settings.FromEmail, _settings.FromName);
            await _fluentEmail
                .To(to)
                .Subject(title)
                .Body(body)
                .SendAsync();
        }
    }
}