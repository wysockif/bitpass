namespace Application.Utils.Email.Templates
{
    public class ChangedPasswordTemplateData : IEmailTemplateData
    {
        public ChangedPasswordTemplateData(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
    }

    public class ChangedPasswordTemplate : IEmailTemplate<ChangedPasswordTemplateData>
    {
        public string RenderTitle(ChangedPasswordTemplateData data)
        {
            return "Your password has been changed";
        }

        public string RenderBody(ChangedPasswordTemplateData data)
        {
            return $"<p>Hello @{data.Username},</p>"
                   + "<p>Your password has been successfully changed. </p>"
                   + "<br/>"
                   + "<p>Bitpass</p>";
        }
    }
}