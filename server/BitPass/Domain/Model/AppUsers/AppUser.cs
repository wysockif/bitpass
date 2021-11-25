using System.Collections.Generic;
using NodaTime;

namespace Domain.Model.AppUsers
{
    public class AppUser : Entity
    {
        public long Id { get; private init; }
        public string Username { get; private init; }
        public string Email { get; private init; }
        public bool IdEmailConfirmed { get; private set; }

        public string PasswordHash { get; private set; }
        public string MasterPasswordHash { get; private set; }
        public Instant? PasswordValidTo { get; private set; }
        public string? PasswordResetTokenHash { get; private set; }
        public Instant? PasswordResetTokenValidTo { get; private set; }
        public List<AppUserSession> Sessions { get; private set; }

        private AppUser(string username, string email, string passwordHash)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
        }

        public static AppUser Register(string username, string email, string password)
        {
            return new AppUser(username, email, password);
        }
        
        public void AddSession(string refreshToken)
        {
            var session = AppUserSession.Create(refreshToken);
            Sessions.Add(session);
        }
        
        public void ResetPassword(string passwordHash)
        {
            PasswordHash = passwordHash;
            PasswordResetTokenHash = null;
            PasswordResetTokenValidTo = null;
        }
    }
}