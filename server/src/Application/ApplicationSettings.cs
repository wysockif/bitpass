using Application.Utils.Security;

namespace Application
{
    public class ApplicationSettings
    {
        public string PasswordPepper { get; set; }
        public string MasterPasswordPepper { get; set; }
        public AccessTokenSettings AccessTokenSettings { get; set; }
        public RefreshTokenSettings RefreshTokenSettings { get; set; }
    }
}