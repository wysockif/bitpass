namespace Application.ViewModels
#pragma warning disable 8618

{
    public class AuthViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public AuthViewModel(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}