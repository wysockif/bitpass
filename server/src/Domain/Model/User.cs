using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;

#pragma warning disable 8618

namespace Domain.Model
{
    public class User : Entity, IAggregateRoot
    {
        private User(string username, string email, string passwordHash, string encryptionKeyHash,
            string emailVerificationTokenHash, DateTime emailVerificationTokenValidTo, string universalToken)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            EncryptionKeyHash = encryptionKeyHash;
            IdEmailConfirmed = false;
            EmailVerificationTokenHash = emailVerificationTokenHash;
            EmailVerificationTokenValidTo = emailVerificationTokenValidTo;
            UniversalToken = universalToken;
            AccountActivities = new List<AccountActivity>();
            Sessions = new List<Session>();
        }

        private User(string username, string email, string passwordHash, string encryptionKeyHash,
            string universalToken)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            EncryptionKeyHash = encryptionKeyHash;
            UniversalToken = universalToken;
            IdEmailConfirmed = false;
            AccountActivities = new List<AccountActivity>();
            Sessions = new List<Session>();
        }

        public long Id { get; private init; }
        public string Username { get; private init; }
        public string Email { get; private init; }
        public string UniversalToken { get; init; }
        public string PasswordHash { get; private set; }
        public string EncryptionKeyHash { get; private set; }
        public string? PasswordResetTokenHash { get; private set; }
        public DateTime? PasswordResetTokenValidTo { get; private set; }
        public bool IdEmailConfirmed { get; private set; }
        public string? EmailVerificationTokenHash { get; private set; }
        public DateTime? EmailVerificationTokenValidTo { get; private set; }
        public List<AccountActivity> AccountActivities { get; private set; }
        public List<Session> Sessions { get; private set; }

        public static User Register(string username, string email, string passwordHash, string encryptionKeyHash,
            string emailVerificationTokenHash, DateTime emailVerificationTokenValidTo, string universalToken)
        {
            return new User(username, email, passwordHash, encryptionKeyHash, emailVerificationTokenHash,
                emailVerificationTokenValidTo, universalToken);
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
            string? osName, string? browserName)
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

        public void AddPasswordResetToken(string passwordResetTokenHash, DateTime passwordResetTokenValidTo)
        {
            PasswordResetTokenHash = passwordResetTokenHash;
            PasswordResetTokenValidTo = passwordResetTokenValidTo;
            Sessions.Clear();
        }

        public void ChangePassword(string passwordHash)
        {
            PasswordHash = passwordHash;
            Sessions.Clear();
        }

        public void DeleteSession(Guid refreshTokenGuid)
        {
            var session = Sessions.FirstOrDefault(s => s.RefreshTokenGuid == refreshTokenGuid);
            if (session == default)
            {
                return;
            }
            Sessions.Remove(session);
        }
    }
}