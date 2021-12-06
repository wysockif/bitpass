namespace Application.Utils.Email.Templates
{
    public class VerifyEmailAddressEmailTemplateData : IEmailTemplateData
    {
    }

    public class VerifyEmailAddressEmailTemplate : IEmailTemplate<VerifyEmailAddressEmailTemplateData>
    {
        public string RenderTitle(VerifyEmailAddressEmailTemplateData data)
        {
            return "Hello darkness my old friends";
        }

        public string RenderBody(VerifyEmailAddressEmailTemplateData data)
        {
            return "I've come to talk with you again";
        }
    }
}