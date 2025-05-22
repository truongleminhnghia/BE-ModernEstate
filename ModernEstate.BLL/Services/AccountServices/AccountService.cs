using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;
using ModernEstate.DAL;
using ModernEstate.BLL.HashPasswords;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Pages;
using ModernEstate.BLL.JWTServices;
using ModernEstate.Common.srcs;

namespace ModernEstate.BLL.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly Utils _utils;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AccountService> logger,
        IPasswordHasher passwordHasher, IJwtService jwtService, Utils utils)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _utils = utils;
        }

        public async Task<bool> CreateAccount(AccountRequest req)
        {
            try
            {
                bool checkRole = string.Equals(_jwtService.GetRole(), EnumRoleName.ROLE_ADMIN.ToString(), StringComparison.OrdinalIgnoreCase);
                if (!checkRole) throw new AppException(ErrorCode.UNAUTHORIZED);
                var existingAccount = await _unitOfWork.Accounts.GetByEmail(req.Email);
                if (existingAccount != null) throw new AppException(ErrorCode.EMAIL_ALREADY_EXISTS);
                var account = _mapper.Map<Account>(req);
                Role? role = await _unitOfWork.Roles.GetByName(req.RoleName);
                if (role == null) throw new AppException(ErrorCode.NOT_FOUND);
                account.Role = role;
                account.RoleId = role.Id;
                account.EnumAccountStatus = EnumAccountStatus.WAIT_CONFIRM;
                account.Password = _passwordHasher.HashPassword(req.Password);
                await _unitOfWork.Accounts.CreateAsync(account);
                await setUpdateByRole(account.Role.RoleName, account.Id);
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

        public async Task<Guid> CreateAccountBrokerOrOwner(AccountRequest req)
        {
            try
            {
                var allowedRoles = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    EnumRoleName.ROLE_BROKER.ToString(),
                    EnumRoleName.ROLE_PROPERTY_OWNER.ToString()
                };
                bool checkRole = allowedRoles.Contains(_jwtService.GetRole());
                if (!checkRole) throw new AppException(ErrorCode.UNAUTHORIZED);
                var existingAccount = await _unitOfWork.Accounts.GetByEmail(req.Email);
                if (existingAccount != null) throw new AppException(ErrorCode.EMAIL_ALREADY_EXISTS);
                var account = _mapper.Map<Account>(req);
                Role? role = await _unitOfWork.Roles.GetByName(req.RoleName);
                if (role == null) throw new AppException(ErrorCode.NOT_FOUND);
                account.Role = role;
                account.RoleId = role.Id;
                account.CreatedAt = DateTime.UtcNow;
                account.UpdatedAt = DateTime.UtcNow;
                account.EnumAccountStatus = EnumAccountStatus.WAIT_CONFIRM;
                account.Password = _passwordHasher.HashPassword(req.Password);
                await _unitOfWork.Accounts.CreateAsync(account);
                await setUpdateByRole(account.Role.RoleName, account.Id);
                await _unitOfWork.SaveChangesWithTransactionAsync();
                return account.OwnerProperty.Id;
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

        private async Task setUpdateByRole(EnumRoleName roleName, Guid accountId)
        {
            switch (roleName)
            {
                case EnumRoleName.ROLE_STAFF:
                case EnumRoleName.ROLE_ADMIN:
                    Employee employee = new Employee
                    {
                        Code = await _utils.GenerateUniqueBrokerCodeAsync("EMP"),
                        AccountId = accountId
                    };
                    await _unitOfWork.Employees.CreateAsync(employee);
                    break;
                case EnumRoleName.ROLE_BROKER:
                    Broker broker = new Broker
                    {
                        Code = await _utils.GenerateUniqueBrokerCodeAsync("BRK"),
                        AccountId = accountId
                    };
                    await _unitOfWork.Brokers.CreateAsync(broker);
                    break;
                case EnumRoleName.ROLE_PROPERTY_OWNER:
                    OwnerProperty owner = new OwnerProperty
                    {
                        Code = await _utils.GenerateUniqueBrokerCodeAsync("OWR"),
                        AccountId = accountId
                    };
                    await _unitOfWork.OwnerProperties.CreateAsync(owner);
                    break;
                default:
                    throw new AppException(ErrorCode.NOT_FOUND, "Role không hợp lệ");
            }
        }

        public async Task<bool> UpdateAccountStatus(Guid id)
        {
            try
            {
                var account = await _unitOfWork.Accounts.GetByIdAsync(id);
                if (account == null) throw new AppException(ErrorCode.USER_NOT_FOUND);
                if (account.EnumAccountStatus == EnumAccountStatus.IN_ACTIVE) throw new AppException(ErrorCode.HAS_INACTIVE);
                account.EnumAccountStatus = EnumAccountStatus.IN_ACTIVE;
                await _unitOfWork.Accounts.UpdateAccount(account);
                await _unitOfWork.SaveChangesWithTransactionAsync();
                _logger.LogInformation("Delete account successfully with id {Id}", id);
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


        public async Task<AccountResponse> GetById(Guid id)
        {
            try
            {
                var validRoles = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
                {
                    EnumRoleName.ROLE_ADMIN.ToString(),
                    EnumRoleName.ROLE_STAFF.ToString()
                };
                bool checkRole = validRoles.Contains(_jwtService.GetRole());
                bool checkId = _jwtService.GetAccountId() == id.ToString() ? true : false;
                if (!checkRole && !checkId) throw new AppException(ErrorCode.UNAUTHORIZED);
                var account = await _unitOfWork.Accounts.FindById(id);
                if (account == null) throw new AppException(ErrorCode.USER_NOT_FOUND);
                var accountResponse = _mapper.Map<AccountResponse>(account);
                return accountResponse;
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

        public async Task<PageResult<AccountResponse>> GetWithParams(string? lastName, string? firstName, EnumAccountStatus? status, EnumRoleName? role, EnumGender? gender, string email, int pageCurrent, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.Accounts.FindWithParams(lastName, firstName, status, role, gender, email);
                if (result == null) throw new AppException(ErrorCode.LIST_EMPTY);
                var pagedResult = result.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                var total = result.Count();
                var data = _mapper.Map<List<AccountResponse>>(pagedResult);
                if (data == null || !data.Any()) throw new AppException(ErrorCode.LIST_EMPTY);
                var pageResult = new PageResult<AccountResponse>(data, pageSize, pageCurrent, total);
                return pageResult;
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

        public async Task<bool> UpdateAccount(UpdateAccountRequest req, Guid id)
        {
            try
            {
                var account = _unitOfWork.Accounts.GetById(id);
                if (account == null) throw new AppException(ErrorCode.USER_NOT_FOUND);
                if (!string.IsNullOrEmpty(req.Email)) account.Email = req.Email;
                if (!string.IsNullOrEmpty(req.FirstName)) account.FirstName = req.FirstName;
                if (!string.IsNullOrEmpty(req.LastName)) account.LastName = req.LastName;
                if (!string.IsNullOrEmpty(req.Phone)) account.Phone = req.Phone;
                if (!string.IsNullOrEmpty(req.EnumAccountStatus.ToString())) account.EnumAccountStatus = req.EnumAccountStatus;
                // if (isAdmin && !string.IsNullOrEmpty(req.Role.ToString())) account.Role = req.Role;
                _unitOfWork.Accounts.Update(account);
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

        public async Task<AccountResponse> GetByEmail(string email)
        {
            try
            {
                var account = await _unitOfWork.Accounts.GetByEmail(email);
                if (account == null) throw new AppException(ErrorCode.USER_NOT_FOUND);
                var accountResponse = _mapper.Map<AccountResponse>(account);
                return accountResponse;
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

        public async Task<AccountResponse> GetByPhone(string phone)
        {
            try
            {
                var account = await _unitOfWork.Accounts.FindByPhone(phone);
                if (account == null) throw new AppException(ErrorCode.USER_NOT_FOUND);
                var accountResponse = _mapper.Map<AccountResponse>(account);
                return accountResponse;
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
