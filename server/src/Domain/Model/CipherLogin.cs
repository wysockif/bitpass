// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Domain.Model
{
    public class CipherLogin : Entity, IAggregateRoot
    {
        private CipherLogin(long ownerId, string identifier, string encryptedPassword, string url)
        {
            OwnerId = ownerId;
            Identifier = identifier;
            EncryptedPassword = encryptedPassword;
            Url = url;
        }

        public long Id { get; private set; }
        public long OwnerId { get; private init; }
        public string Identifier { get; private init; }
        public string EncryptedPassword { get; private init; }
        public string Url { get; private init; }

        public static CipherLogin Create(long ownerId, string identifier, string encryptedPassword, string url)
        {
            return new CipherLogin(ownerId, identifier, encryptedPassword, url);
        }
    }
}