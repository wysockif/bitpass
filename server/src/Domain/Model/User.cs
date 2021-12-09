using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;

#pragma warning disable 8618

namespace Domain.Model
{
    public class User : Entity, IAggregateRoot
    {
        private User(string username, string email, string passwordHash, string masterPasswordHash,
            string emailVerificationTokenHash, DateTime emailVerificationTokenValidTo)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            MasterPasswordHash = masterPasswordHash;
            IdEmailConfirmed = false;
            EmailVerificationTokenHash = emailVerificationTokenHash;
            EmailVerificationTokenValidTo = emailVerificationTokenValidTo;
            AccountActivities = new List<AccountActivity>();
            Sessions = new List<Session>();
        }

        private User(string username, string email, string passwordHash, string masterPasswordHash)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            MasterPasswordHash = masterPasswordHash;
            IdEmailConfirmed = false;
            AccountActivities = new List<AccountActivity>();
            Sessions = new List<Session>();
        }

        public long Id { get; private init; }
        public string Username { get; private init; }
        public string Email { get; private init; }
        public string PasswordHash { get; private set; }
        public string MasterPasswordHash { get; private set; }
        public string? PasswordResetTokenHash { get; private set; }
        public DateTime? PasswordResetTokenValidTo { get; private set; }
        public bool IdEmailConfirmed { get; private set; }
        public string? EmailVerificationTokenHash { get; private set; }
        public DateTime? EmailVerificationTokenValidTo { get; private set; }
        public List<AccountActivity> AccountActivities { get; private set; }
        public List<Session> Sessions { get; private set; }

        public static User Register(string username, string email, string passwordHash, string masterPasswordHash,
            string emailVerificationTokenHash, DateTime emailVerificationTokenValidTo)
        {
            return new User(username, email, passwordHash, masterPasswordHash, emailVerificationTokenHash,
                emailVerificationTokenValidTo);
        }

        public void ResetPassword(string passwordHash)
        {
            PasswordHash = passwordHash;
            PasswordResetTokenHash = null;
            PasswordResetTokenValidTo = null;
        }

        public void AddAccountActivity(ActivityType activityType, string? ipAddress, string? osName,
            string? browserName)
        {
            var accountActivity = AccountActivity.Create(Id, activityType, ipAddress, osName, browserName);
            AccountActivities.Add(accountActivity);
        }

        public void AddSession(Guid refreshTokenGuid, long refreshTokenUnixExpirationDate, string? ipAddress,
            string? osName,
            string? browserName)
        {
            var session = Session.Create(Id, refreshTokenGuid, refreshTokenUnixExpirationDate, ipAddress, osName,
                browserName);
            Sessions.Add(session);
        }

        public void UpdateSession(Guid oldRefreshTokenGuid, Guid newRefreshTokenTokenGuid,
            long newRefreshTokenExpirationTimestamp)
        {
            var session = Sessions.FirstOrDefault(s => s.RefreshTokenGuid == oldRefreshTokenGuid);
            if (session == default || session.ExpirationUnixTimestamp < DateTimeOffset.Now.ToUnixTimeSeconds())
            {
                throw new AuthenticationException("Session for this refresh token does not exist or is not active");
            }

            session.Update(newRefreshTokenTokenGuid, newRefreshTokenExpirationTimestamp);
        }

        public void VerifyEmail()
        {
            IdEmailConfirmed = true;
            EmailVerificationTokenHash = null;
            EmailVerificationTokenValidTo = null;
        }
    }
}