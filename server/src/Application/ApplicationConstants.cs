namespace Application
{
    public class ApplicationConstants
    {
        public const int MinPasswordLength = 8;
        public const int MaxPasswordLength = 256;
        public const int MinUsernameLength = 3;
        public const int MaxUsernameLength = 64;
        public const int MinEmailLength = 5;
        public const int MaxEmailLength = 64;
        public const int InvalidLoginDelayInMilliseconds = 2000;
        public const int UniversalTokenLength = 64;
        public const int PasswordResetTokenLength = 64;
        public const int PasswordResetTokenDurationInMinutes = 15;
        public const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    }
}