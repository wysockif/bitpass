namespace Application.Utils.Email.Templates
{
    public class VerifyEmailAddressEmailTemplateData : IEmailTemplateData
    {
        public VerifyEmailAddressEmailTemplateData(string username, string url)
        {
            Username = username;
            Url = url;
        }

        public string Username { get; set; }
        public string Url { get; set; }
    }

    public class VerifyEmailAddressEmailTemplate : IEmailTemplate<VerifyEmailAddressEmailTemplateData>
    {
        public string RenderTitle(VerifyEmailAddressEmailTemplateData data)
        {
            return "Verify your email address";
        }

        public string RenderBody(VerifyEmailAddressEmailTemplateData data)
        {
            return $"<p>Hello @{data.Username},</p>"
                   + "<p>Click the link below to verify your email address: </p>"
                   + $"<a href=\"{data.Url}\">{data.Url}</a>"
                   + "<br/>"
                   + "<p>Bitpass</p>";
        }
    }
}