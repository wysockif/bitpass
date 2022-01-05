#pragma warning disable 8618
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Infrastructure.Mailing
{
    public class SendGridSettings
    {
        public string ApiKey { get; set; }
        public string FromName { get; set; }
        public string FromEmail { get; set; }
    }
}