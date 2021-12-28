using System.Collections.Generic;

namespace Application.ViewModels
{
    public class SessionListViewModel
    {
        public IEnumerable<SessionViewModel> Items { get; set; }

        public SessionListViewModel(IEnumerable<SessionViewModel> items)
        {
            Items = items;
        }
    }
}