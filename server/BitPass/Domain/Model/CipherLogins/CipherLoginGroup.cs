using System.Collections.Generic;

namespace Domain.Model.CipherLogins
{
    public class CipherLoginGroup : IAggregateRoot
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<CipherLogin> Passwords { get; private set; }
    }
}