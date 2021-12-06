namespace Infrastructure.Mailing
{
    public class SendGridSettings
    {
        public string ApiKey { get; set; }

        public string FromName { get; set; }

        public string FromEmail { get; set; }
    }
}