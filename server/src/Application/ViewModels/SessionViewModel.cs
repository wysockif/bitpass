namespace Application.ViewModels
{
    public class SessionViewModel
    {
        public long Id { get; set; }
        public long ExpirationUnixTimestamp { get; set; }
        public string? OsName { get; set; }
        public string? BrowserName { get; set; }
        public string? IpAddress { get; set; }
    }
}