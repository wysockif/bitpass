namespace Application.ViewModels
{
    public class CipherLoginViewModel
    {
        public long Id { get; set; }
        public string Identifier { get; set; }
        public string EncryptedPassword { get; set; }
        public string Url { get; set; }

        public CipherLoginViewModel(long id, string identifier, string encryptedPassword, string url)
        {
            Id = id;
            Identifier = identifier;
            EncryptedPassword = encryptedPassword;
            Url = url;
        }
    }
}