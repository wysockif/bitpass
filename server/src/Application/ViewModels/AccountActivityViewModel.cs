using System;

#pragma warning disable 8618

namespace Application.ViewModels
{
    public class AccountActivityViewModel
    {
        public DateTime CreatedAt { get; set; }
        public string? OsName { get; set; }
        public string? BrowserName { get; set; }
        public string? IpAddress { get; set; }
        public string ActivityType { get; set; }
    }
}