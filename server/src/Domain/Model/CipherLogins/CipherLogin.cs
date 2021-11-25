namespace Domain.Model.CipherLogins
{
    public class CipherLogin : Entity, IAggregateRoot
    {
        public long Id { get; private set; }
        public long OwnerId { get; private init; }
        public string Identifier { get; private init; }
        public string EncryptedPassword { get; private init; }
        public string Url { get; private init; }

        private CipherLogin(long ownerId, string identifier, string encryptedPassword, string url)
        {
            OwnerId = ownerId;
            Identifier = identifier;
            EncryptedPassword = encryptedPassword;
            Url = url;
        }

        public static CipherLogin Create(long ownerId, string identifier, string encryptedPassword, string url)
        {
            return new CipherLogin(ownerId, identifier, encryptedPassword, url);
        }
    }
}