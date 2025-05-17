using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.JWTServices
{
    public interface IJwtService
    {
        public string GenerateJwtToken(Account _account);
        public int? ValidateToken(string token);
        public string GetAccountId();
        public string GetEmail();
        public string GetRole();
        public string GetTokenId();
    }
}
