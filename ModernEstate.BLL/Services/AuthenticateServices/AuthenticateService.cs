using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.BLL.JWTServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.AuthenticateResponse;
using ModernEstate.DAL.Entites;
using ModernEstate.DAL;
using ModernEstate.BLL.HashPasswords;
using ModernEstate.Common.Models.Requests;

namespace ModernEstate.BLL.Services.AuthenticateServices
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticateService> _logger;

        public AuthenticateService(IUnitOfWork unitOfWork, IJwtService jwtService, IPasswordHasher passwordHasher, IMapper mapper, ILogger<AuthenticateService> logger)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AuthenticateResponse> Login(string email, string password)
        {
            try
            {
                Account? account = await _unitOfWork.Accounts.GetByEmail(email);
                if (account == null)
                {
                    throw new Exception("Account not found.");
                }
                bool checkPassword = _passwordHasher.VerifyPassword(password, account.Password);
                if (!checkPassword)
                {
                    throw new Exception("Invalid password.");
                }
                var token = _jwtService.GenerateJwtToken(account);
                var currentAccount = _mapper.Map<AccountCurrent>(account);
                var authenticateResponse = new AuthenticateResponse
                {
                    Token = token,
                    AccountCurrent = currentAccount
                };
                return authenticateResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while logging in.", ex);
            }
        }

        // public async Task<bool> Register(RegisterRequest request)
        // {
        //     try
        //     {
        //         var existingAccount = await _unitOfWork.Accounts.GetByEmail(request.Email);
        //         if (existingAccount != null)
        //         {
        //             _logger.LogWarning("Không thể tạo tài khoản, email {Email} đã tồn tại.", request.Email);
        //             return false;
        //         }
        //         var account = _mapper.Map<Account>(request);
        //         account.Role = EnumRoleName.ROLE_CUSTOMER;
        //         account.EnumAccountStatus = EnumAccountStatus.WAIT_CONFIRM;
        //         account.Password = _passwordHasher.HashPassword(request.Password);
        //         await _unitOfWork.Accounts.CreateAsync(account);
        //         await _unitOfWork.SaveChangesWithTransactionAsync();
        //         _logger.LogInformation("Tạo tài khoản mới thành công với email {Email}, Role: {Role}", request.Email, account.Role);
        //         return true;
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "Lỗi xảy ra khi tạo tài khoản với email {Email}", request.Email);
        //         return false;
        //     }
        // }
    }
}
