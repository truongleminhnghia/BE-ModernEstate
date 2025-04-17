
namespace ModernEstate.Common.Models.AuthenticateResponse
{
    public class AuthenticateResponse
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public AccountCurrent? AccountCurrent { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
