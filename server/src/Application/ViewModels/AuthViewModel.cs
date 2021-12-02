namespace Application.ViewModels
#pragma warning disable 8618

{
    public class AuthViewModel
    {
        public string AccessToken { get; set; }

        public AuthViewModel(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}