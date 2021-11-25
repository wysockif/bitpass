using System.Collections.Generic;

namespace Domain.Model.CipherLogins
{
    public class CipherLogin : Entity, IAggregateRoot
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public string Identifier { get; set; }
        public string EncryptedPassword { get; set; }
        public string Url { get; set; }
    }
}