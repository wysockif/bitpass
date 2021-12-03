using System;

namespace Domain.Model
{
    public class Session
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public Guid RefreshTokenId { get; set; }
        public long RefreshTokenExpirationUnixTimestamp { get; set; }
        public string? OsName { get; private init; }
        public string? BrowserName { get; private init; }
        public string? IpAddress { get; private init; }

        private Session(long userId, Guid refreshTokenId, long refreshTokenExpirationUnixTimestamp, string? osName,
            string? browserName, string? ipAddress)
        {
            UserId = userId;
            RefreshTokenId = refreshTokenId;
            RefreshTokenExpirationUnixTimestamp = refreshTokenExpirationUnixTimestamp;
            OsName = osName;
            BrowserName = browserName;
            IpAddress = ipAddress;
        }

        public static Session Create(long userId, Guid refreshTokenGuid, long refreshTokenUnixExpirationDate,
            string? ipAddress, string? osName, string? browserName)
        {
            return new Session(userId, refreshTokenGuid, refreshTokenUnixExpirationDate, osName, browserName, ipAddress);
        }
    }
}