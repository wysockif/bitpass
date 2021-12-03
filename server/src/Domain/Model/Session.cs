using System;

namespace Domain.Model
{
    public class Session
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public Guid RefreshTokenGuid { get; set; }
        public long ExpirationUnixTimestamp { get; set; }
        public string? OsName { get; private init; }
        public string? BrowserName { get; private init; }
        public string? IpAddress { get; private init; }

        private Session(long userId, Guid refreshTokenGuid, long expirationUnixTimestamp, string? osName,
            string? browserName, string? ipAddress)
        {
            UserId = userId;
            RefreshTokenGuid = refreshTokenGuid;
            ExpirationUnixTimestamp = expirationUnixTimestamp;
            OsName = osName;
            BrowserName = browserName;
            IpAddress = ipAddress;
        }

        public static Session Create(long userId, Guid refreshTokenGuid, long expirationUnixTimestamp,
            string? ipAddress, string? osName, string? browserName)
        {
            return new Session(userId, refreshTokenGuid, expirationUnixTimestamp, osName, browserName, ipAddress);
        }

        public void Update(Guid newRefreshTokenTokenGuid, long newRefreshTokenExpirationTimestamp)
        {
            RefreshTokenGuid = newRefreshTokenTokenGuid;
            ExpirationUnixTimestamp = newRefreshTokenExpirationTimestamp;
        }
    }
}