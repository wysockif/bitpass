using System.Collections.Generic;

namespace Application.ViewModels
{
    public class VaultViewModel
    {
        public IEnumerable<CipherLoginViewModel> Items { get; set; }

        public VaultViewModel(IEnumerable<CipherLoginViewModel> items)
        {
            Items = items;
        }
    }
}