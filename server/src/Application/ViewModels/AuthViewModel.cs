namespace Application.ViewModels
#pragma warning disable 8618

{
    public class AuthViewModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string UniversalToken { get; set; }

        public AuthViewModel(string accessToken, string refreshToken, string universalToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            UniversalToken = universalToken;
        }
    }
}