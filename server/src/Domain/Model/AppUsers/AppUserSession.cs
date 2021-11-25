namespace Domain.Model.AppUsers
{
    public class AppUserSession
    {
        public long Id { get; private set; }
        public string RefreshToken { get; private set; }


        public string? IpAddress { get; private init; }

        private AppUserSession(string refreshToken, string? ipAddress)
        {
            RefreshToken = refreshToken;
            IpAddress = ipAddress;
        }

        public static AppUserSession Create(string refreshToken, string? ipAddress = null)
        {
            return new AppUserSession(refreshToken, ipAddress);
        }

        public void UpdateRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}