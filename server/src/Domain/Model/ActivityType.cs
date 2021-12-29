namespace Domain.Model
{
    public enum ActivityType
    {
        Login,
        FailedLogin,
        Registration,
        FailedPasswordChange,
        PasswordChanged,
        PasswordResetRequested,
        FailedPasswordReset,
        PasswordReset,
        FailedAddressEmailVerification,
        EmailVerified
    }
}