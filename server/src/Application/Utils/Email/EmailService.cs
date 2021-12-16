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
        private readonly ChangedPasswordTemplate _changedPasswordTemplate;
        private readonly IEmailGateway _emailGateway;
        private readonly RequestResetPasswordTemplate _requestResetPasswordTemplate;
        private readonly VerifyEmailAddressEmailTemplate _verifyEmailAddressEmailTemplate;

        public EmailService(IEmailGateway emailGateway, VerifyEmailAddressEmailTemplate verifyEmailAddressEmailTemplate,
            RequestResetPasswordTemplate requestResetPasswordTemplate, ChangedPasswordTemplate changedPasswordTemplate)
        {
            _emailGateway = emailGateway;
            _verifyEmailAddressEmailTemplate = verifyEmailAddressEmailTemplate;
            _requestResetPasswordTemplate = requestResetPasswordTemplate;
            _changedPasswordTemplate = changedPasswordTemplate;
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
                RequestResetPasswordTemplateData data => new
                {
                    Title = _requestResetPasswordTemplate.RenderTitle(data),
                    Body = _requestResetPasswordTemplate.RenderBody(data)
                },
                ChangedPasswordTemplateData data => new
                {
                    Title = _changedPasswordTemplate.RenderTitle(data),
                    Body = _changedPasswordTemplate.RenderBody(data)
                },
                _ => throw new Exception("Email template not supported")
            };
            await _emailGateway.SendEmailAsync(to, emailData.Title, emailData.Body);
        }
    }
}