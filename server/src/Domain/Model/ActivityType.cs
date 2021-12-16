namespace Domain.Model
{
    public enum ActivityType
    {
        SuccessfulLogin,
        FailedLogin,
        SuccessfulRegistration,
        InvalidOldPasswordDuringPasswordChange,
        PasswordChanged,
        PasswordChangeRequested,
        InvalidTokenDuringPasswordReset
    }
}