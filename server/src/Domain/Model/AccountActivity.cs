using System;

namespace Domain.Model
{
    public class AccountActivity
    {
        public long Id { get; private set; }
        
        public long UserId { get; init; }
        public DateTime CreatedAt {  get; private init; }
        public string? OsName { get; private init; }
        public string? BrowserName { get; private init; }
        public string? IpAddress { get; private init; }
        
        public ActivityType ActivityType { get; private set; }

        private AccountActivity(long userId, ActivityType activityType, DateTime createdAt, string? ipAddress, string? osName, string? browserName)
        {
            UserId = userId;
            ActivityType = activityType;
            CreatedAt = createdAt;
            IpAddress = ipAddress;
            OsName = osName;
            BrowserName = browserName;
        }

        public static AccountActivity Create(long userId, ActivityType activityType, string? ipAddress, string? osName, string? browserName)
        {
            return new AccountActivity(userId, activityType, DateTime.Now, ipAddress, osName, browserName);
        }
    }
}