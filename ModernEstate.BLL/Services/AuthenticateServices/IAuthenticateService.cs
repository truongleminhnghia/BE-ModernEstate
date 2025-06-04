
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.AuthenticateResponse;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.AuthenticateServices
{
    public interface IAuthenticateService
    {
        public Task<AuthenticateResponse> Login(string email, string password);
        public Task<bool> Register(RegisterRequest request);
        public Task<bool> ChangePassword(string oldPassword, string newPassword, Guid id);

        public Task<bool> VerifyEmailAsync(string token);
        public Task<ForgetPasswordResponse> ForgotPasswordAsync(string email);
        public Task<ForgetPasswordResponse> ResetPasswordAsync(string token, string newPassword);
    }
}
