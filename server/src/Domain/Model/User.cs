using System;
using System.Collections.Generic;
#pragma warning disable 8618

namespace Domain.Model
{
    public class User : Entity, IAggregateRoot
    {
        public long Id { get; private init; }
        public string Username { get; private init; }
        public string Email { get; private init; }
        public bool IdEmailConfirmed { get; private set; }
        public string PasswordHash { get; private set; }
        public string MasterPasswordHash { get; private set; }
        public string? PasswordResetTokenHash { get; private set; }
        public DateTime? PasswordResetTokenValidTo { get; private set; }
        public List<AccountActivity> AccountActivities { get; private set; }
        public List<Session> Sessions { get; private set; }
        
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

        public static User Register(string username, string email, string passwordHash, string masterPasswordHash)
        {
            return new User(username, email, passwordHash, masterPasswordHash);
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

        public void AddSession(Guid refreshTokenGuid, long refreshTokenUnixExpirationDate, string? ipAddress, string? osName,
        string? browserName)
        {
            var session = Session.Create(Id, refreshTokenGuid, refreshTokenUnixExpirationDate, ipAddress, osName,
                browserName);
            Sessions.Add(session);
        }
    }
}