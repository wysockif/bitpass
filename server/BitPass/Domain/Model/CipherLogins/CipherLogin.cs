using System.Collections.Generic;

namespace Domain.Model.CipherLogins
{
    public class CipherLogin : Entity
    {
        public long Id { get; set; }
        public string Identifier { get; set; }
        public string EncryptedPassword { get; set; }
        public string Url { get; set; }
        public List<CipherLoginGroup> CipherLoginGroups { get; set; }
    }
}