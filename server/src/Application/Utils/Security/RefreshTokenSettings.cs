namespace Application.Utils.Security
{
    public class RefreshTokenSettings
    {
        public string Key { get; set; }
        public int ExpiryTimeInHours { get; set; }
    }
}