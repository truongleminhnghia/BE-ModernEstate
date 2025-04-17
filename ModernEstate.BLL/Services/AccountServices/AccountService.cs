using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;
using ModernEstate.DAL;
using ModernEstate.BLL.HashPasswords;

namespace ModernEstate.BLL.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;
        private readonly IPasswordHasher _passwordHasher;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AccountService> logger, IPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> CreateAccount(AccountRequest req, bool isAdmin)
        {
            try
            {
                var existingAccount = await _unitOfWork.Accounts.GetByEmail(req.Email);
                if (existingAccount != null)
                {
                    _logger.LogWarning("Không thể tạo tài khoản, email {Email} đã tồn tại.", req.Email);
                    return false;
                }
                var account = _mapper.Map<Account>(req);
                account.Role = isAdmin ? req.Role : EnumRoleName.ROLE_CUSTOMER;
                account.EnumAccountStatus = EnumAccountStatus.WAIT_CONFIRM;
                account.Password = _passwordHasher.HashPassword(req.Password);
                await _unitOfWork.Accounts.CreateAsync(account);
                await _unitOfWork.SaveChangesWithTransactionAsync();
                _logger.LogInformation("Tạo tài khoản mới thành công với email {Email}, Role: {Role}", req.Email, account.Role);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi xảy ra khi tạo tài khoản với email {Email}", req.Email);
                return false;
            }
        }

        public async Task<AccountResponse> GetById(Guid id)
        {
            try
            {

                // Lấy tài khoản từ repository
                var account = await _unitOfWork.Accounts.GetByIdAsync(id);

                // Nếu không tìm thấy tài khoản
                if (account == null)
                {
                    return null; // Trả về null nếu không tìm thấy
                }

                // Chuyển đổi từ Entity sang DTO để trả về
                return new AccountResponse
                {
                    Id = account.Id,
                    Email = account.Email,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Phone = account.Phone,
                    Address = account.Address,
                    Role = account.Role
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
