using System.Collections.Generic;

namespace Application.ViewModels
{
    public class AccountActivityListViewModel
    {
        public IEnumerable<AccountActivityViewModel> Items { get; set; }

        public AccountActivityListViewModel(IEnumerable<AccountActivityViewModel> items)
        {
            Items = items;
        }
    }
}