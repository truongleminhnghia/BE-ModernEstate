using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ModernEstate.BLL.HashPasswords;
using ModernEstate.BLL.JWTServices;
using ModernEstate.BLL.Services.EmailServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.AuthenticateResponse;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;
using System.Security.Claims;

namespace ModernEstate.BLL.Services.AuthenticateServices
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<AuthenticateService> _logger;

        public AuthenticateService(IUnitOfWork unitOfWork, IJwtService jwtService, IPasswordHasher passwordHasher, IMapper mapper, ILogger<AuthenticateService> logger, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _logger = logger;
            _emailService = emailService;
        }

        public async Task<bool> ChangePassword(string oldPassword, string newPassword, Guid id)
        {
            try
            {
                if (string.IsNullOrEmpty(id.ToString())) throw new AppException(ErrorCode.UNAUTHORIZED);
                var account = await _unitOfWork.Accounts.GetByIdAsync(id);
                if (account == null) throw new AppException(ErrorCode.NOT_FOUND);
                bool checkPassword = _passwordHasher.VerifyPassword(oldPassword, account.Password);
                if (!checkPassword) throw new AppException(ErrorCode.INVALID_PASSWORD);
                account.Password = _passwordHasher.HashPassword(newPassword);
                await _unitOfWork.Accounts.UpdateAsync(account);
                await _unitOfWork.SaveChangesWithTransactionAsync();
                return true;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<AuthenticateResponse> Login(string email, string password)
        {
            try
            {
                Account? account = await _unitOfWork.Accounts.GetByEmail(email);
                if (account == null) throw new AppException(ErrorCode.EMAIL_DO_NOT_EXISTS);
                bool checkPassword = _passwordHasher.VerifyPassword(password, account.Password);
                if (!checkPassword) throw new AppException(ErrorCode.INVALID_PASSWORD);
                var token = _jwtService.GenerateJwtToken(account);
                if (string.IsNullOrEmpty(token)) throw new AppException(ErrorCode.TOKEN_NOT_NULL);
                var currentAccount = _mapper.Map<AccountCurrent>(account);
                var expiredAt = _jwtService.GetExpire(token);
                var authenticateResponse = new AuthenticateResponse
                {
                    Token = token,
                    AccountCurrent = currentAccount,
                    ExpiredAt = expiredAt
                };
                return authenticateResponse;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            try
            {
                var existingAccount = await _unitOfWork.Accounts.GetByEmail(request.Email);
                if (existingAccount != null)
                {
                    _logger.LogWarning("Không thể tạo tài khoản, email {Email} đã tồn tại.", request.Email);
                    throw new AppException(ErrorCode.EMAIL_ALREADY_EXISTS);
                }
                var account = _mapper.Map<Account>(request);
                Role? role = await _unitOfWork.Roles.GetByName(EnumRoleName.ROLE_CUSTOMER);
                if (role == null) throw new AppException(ErrorCode.ROLE_NOT_NULL);
                account.RoleId = role.Id;
                account.Role = role;
                account.EnumAccountStatus = EnumAccountStatus.WAIT_CONFIRM;
                if (account.EnumAccountStatus == null) throw new AppException(ErrorCode.ACCOUNT_STATUS_NOT_NULL);
                if (request.Password != request.ConfirmPassword) throw new AppException(ErrorCode.INVALID_PASSWORD);
                account.Password = _passwordHasher.HashPassword(request.Password);
                await _unitOfWork.Accounts.CreateAsync(account);
                await _unitOfWork.SaveChangesWithTransactionAsync();
                string token = _jwtService.GenerateEmailVerificationToken(account.Email!);

                string verifyUrl = $"https://bemodernestate.site//api/v1/auths/verify-email?token={token}";


                await _emailService.SendEmailAsync(account.Email!, "Xác minh email", verifyUrl);
                _logger.LogInformation("Tạo tài khoản mới thành công với email {Email}, Role: {Role}", request.Email, account.Role);
                return true;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> VerifyEmailAsync(string token)
        {
            try
            {
                var principal = _jwtService.ValidateTokenClaimsPrincipal(token);
                var email = principal.FindFirst(ClaimTypes.Email)?.Value;

                if (email == null)
                    throw new AppException(ErrorCode.EMAIL_DO_NOT_EXISTS);

                var account = await _unitOfWork.Accounts.GetByEmail(email);
                if (account == null)
                    throw new AppException(ErrorCode.INVALID_ACCOUNT_ROLE);

                if (account.EnumAccountStatus == EnumAccountStatus.ACTIVE)
                    return false;

                account.EnumAccountStatus = EnumAccountStatus.ACTIVE;
                await _unitOfWork.SaveChangesWithTransactionAsync();
                return true;
            }
            catch (SecurityTokenExpiredException)
            {
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
            catch (Exception)
            {
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<ForgetPasswordResponse> ForgotPasswordAsync(string email)
        {
            var account = await _unitOfWork.Accounts.GetByEmail(email);
            if (account == null)
                return ForgetPasswordResponse.Fail("Email not found");

            Random random = new Random();
            string otp = random.Next(100000, 1000000).ToString();
            account.PasswordResetToken = otp;
            account.PasswordResetTokenExpiry = DateTime.UtcNow.AddMinutes(15);

            await _unitOfWork.Accounts.UpdateAsync(account);
            await _unitOfWork.SaveChangesWithTransactionAsync();


            await _emailService.SendEmailResetPasswordAsync(email, "Reset Your Password", otp);

            return ForgetPasswordResponse.Ok("Password reset OTP has been sent to your email.");
        }

        public async Task<ForgetPasswordResponse> ResetPasswordAsync(string token, string newPassword)
        {
            var account = await _unitOfWork.Accounts.GetByResetTokenAsync(token);
            if (account == null || account.PasswordResetTokenExpiry < DateTime.UtcNow)
                return ForgetPasswordResponse.Fail("Invalid or expired password reset token.");

            account.Password = _passwordHasher.HashPassword(newPassword);
            account.PasswordResetToken = null;
            account.PasswordResetTokenExpiry = null;

            await _unitOfWork.Accounts.UpdateAsync(account);
            await _unitOfWork.SaveChangesWithTransactionAsync();

            return ForgetPasswordResponse.Ok("Password has been reset successfully.");
        }

        public async Task<bool> ResendVerificationEmailAsync(string email)
        {
            try
            {
                var account = await _unitOfWork.Accounts.GetByEmail(email);
                if (account == null)
                {
                    _logger.LogWarning("Email {Email} không tồn tại.", email);
                    throw new AppException(ErrorCode.EMAIL_DO_NOT_EXISTS);
                }


                string token = _jwtService.GenerateEmailVerificationToken(account.Email!);
                string verifyUrl = $"https://bemodernestate.site/api/v1/auths/verify-email?token={token}";

                await _emailService.SendEmailAsync(account.Email!, "Xác minh email", verifyUrl);
                _logger.LogInformation("Đã gửi lại email xác minh cho {Email}", email);
                return true;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }



    }
}
