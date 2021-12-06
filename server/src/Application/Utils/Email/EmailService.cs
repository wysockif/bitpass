using System;
using System.Threading.Tasks;
using Application.InfrastructureInterfaces;
using Application.Utils.Email.Templates;

namespace Application.Utils.Email
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, IEmailTemplateData templateData);
    }

    public class EmailService : IEmailService
    {
        private readonly IEmailGateway _emailGateway;
        private readonly VerifyEmailAddressEmailTemplate _verifyEmailAddressEmailTemplate;

        public EmailService(IEmailGateway emailGateway, VerifyEmailAddressEmailTemplate verifyEmailAddressEmailTemplate)
        {
            _emailGateway = emailGateway;
            _verifyEmailAddressEmailTemplate = verifyEmailAddressEmailTemplate;
        }

        public async Task SendEmailAsync(string to, IEmailTemplateData templateData)
        {
            var emailData = templateData switch
            {
                VerifyEmailAddressEmailTemplateData data => new
                {
                    Title = _verifyEmailAddressEmailTemplate.RenderTitle(data),
                    Body = _verifyEmailAddressEmailTemplate.RenderBody(data)
                },
                _ => throw new Exception("Email template not supported")
            };
            await _emailGateway.SendEmailAsync(to, emailData.Title, emailData.Body);
        }
    }
}