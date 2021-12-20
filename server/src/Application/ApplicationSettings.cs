using Application.Utils.Security;

namespace Application
{
    public class ApplicationSettings
    {
        public string FrontendUrl { get; set; }
        public string PasswordHashPepper { get; set; }
        public string EncryptionKeyHashPepper { get; set; }
        public AccessTokenSettings AccessTokenSettings { get; set; }
        public RefreshTokenSettings RefreshTokenSettings { get; set; }
    }
}