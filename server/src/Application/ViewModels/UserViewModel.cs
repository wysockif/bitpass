namespace Application.ViewModels
#pragma warning disable 8618

{
    public class UserViewModel
    {
        public long Id { get; private init; }
        public string Username { get; private init; }
        public string Email { get; private init; }
        public bool IdEmailConfirmed { get; private set; }
    }
}