using System;
using NodaTime;

namespace Domain.Model
{
    public class SecurityActivity
    {
        public long Id { get; private set; }
        public DateTime LoggedAt {  get; private init; }
        public string? IpAddress { get; private init; }

        private SecurityActivity(DateTime loggedAt, string? ipAddress)
        {
            LoggedAt = loggedAt;
            IpAddress = ipAddress;
        }

        public static SecurityActivity Create(string? ipAddress = null)
        {
            return new SecurityActivity(DateTime.Now, ipAddress);
        }
    }
}