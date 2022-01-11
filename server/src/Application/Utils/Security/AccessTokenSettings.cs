// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

#pragma warning disable 8618
namespace Application.Utils.Security
{
    public class AccessTokenSettings
    {
        public string Key { get; set; }
        public int ExpiryTimeInMinutes { get; set; }
    }
}