
using ModernEstate.Common.Models.AuthenticateResponse;
using ModernEstate.Common.Models.Requests;

namespace ModernEstate.BLL.Services.AuthenticateServices
{
    public interface IAuthenticateService
    {
        public Task<AuthenticateResponse> Login(string email, string password);
        public Task<bool> Register(RegisterRequest request);
        public Task<bool> ChangePassword(string oldPassword, string newPassword, Guid id);
    }
}
