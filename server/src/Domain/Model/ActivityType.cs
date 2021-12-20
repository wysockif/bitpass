namespace Domain.Model
{
    public enum ActivityType
    {
        SuccessfulLogin,
        FailedLogin,
        Registration,
        FailedPasswordChange,
        PasswordChanged,
        PasswordResetRequested,
        FailedPasswordReset,
        PasswordResetFailed,
        SuccessfulPasswordReset,
        FailedAddressEmailVerification,
        EmailVerified
    }
}