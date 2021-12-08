using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
using FluentEmail.Core;

namespace Infrastructure.Mailing
{
    public class SendGridEmailGateway : IEmailGateway
    {
        private readonly IFluentEmail _fluentEmail;

        public SendGridEmailGateway(SendGridSettings settings, IFluentEmail fluentEmail)
        {
            _fluentEmail = fluentEmail;
            _fluentEmail.SetFrom(settings.FromEmail, settings.FromName);
        }

        public async Task SendEmailAsync(string to, string title, string body)
        {
            await _fluentEmail
                .To(to)
                .Subject(title)
                .Body(body)
                .SendAsync();
        }
    }
}