namespace Application.Utils.Email.Templates
{
    public class RequestResetPasswordTemplateData : IEmailTemplateData
    {
        public RequestResetPasswordTemplateData(string username, string url)
        {
            Username = username;
            Url = url;
        }

        public string Username { get; set; }
        public string Url { get; set; }
    }

    public class RequestResetPasswordTemplate : IEmailTemplate<RequestResetPasswordTemplateData>
    {
        public string RenderTitle(RequestResetPasswordTemplateData data)
        {
            return "Reset your password";
        }

        public string RenderBody(RequestResetPasswordTemplateData data)
        {
            return $"<p>Hello @{data.Username},</p>"
                   + "<p>You have requested to reset your password. </p>"
                   + "<p>Click the link below to do so: </p>"
                   + $"<a href=\"{data.Url}\">{data.Url}</a>"
                   + "<br/>"
                   + "<p>Bitpass</p>";
        }
    }
}