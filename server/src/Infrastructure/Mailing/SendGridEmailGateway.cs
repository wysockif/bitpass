using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
using FluentEmail.Core;

namespace Infrastructure.Mailing
{
    public class SendGridEmailGateway : IEmailGateway
    {
        private readonly IFluentEmailFactory _fluentEmailFactory;
        private readonly SendGridSettings _settings;

        public SendGridEmailGateway(SendGridSettings settings, IFluentEmailFactory fluentEmailFactory)
        {
            _settings = settings;
            _fluentEmailFactory = fluentEmailFactory;
        }

        public async Task SendEmailAsync(string to, string title, string body)
        {
            await _fluentEmailFactory
                .Create()
                .SetFrom(_settings.FromEmail, _settings.FromName)
                .To(to)
                .Subject(title)
                .Body(body, true)
                .SendAsync();
        }
    }
}