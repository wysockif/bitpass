using System;
using Domain.Model;

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