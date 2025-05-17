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

        public string GenerateRandomNumber()
        {
            Random random = new Random();
            string randomNumber = random.Next(10000000, 99999999).ToString(); // Tạo 1 số ngẫu nhiên từ 10000000 đến 99999999
            return randomNumber;
        }


        public async Task<bool> CreateAccount(AccountRequest req, bool isAdmin)
        {
            try
            {
                var existingAccount = await _unitOfWork.Accounts.GetByEmail(req.Email);
                if (existingAccount != null) throw new AppException(ErrorCode.EMAIL_ALREADY_EXISTS);
                var account = _mapper.Map<Account>(req);
                // account.Role = isAdmin ? req.Role : EnumRoleName.ROLE_CUSTOMER;
                Role? role = await _unitOfWork.Roles.GetByName(req.RoleName);
                if (role == null) throw new AppException(ErrorCode.NOT_FOUND);
                account.Role = role;
                account.RoleId = role.Id;
                account.CreatedAt = DateTime.UtcNow;
                account.UpdatedAt = DateTime.UtcNow;
                account.EnumAccountStatus = EnumAccountStatus.WAIT_CONFIRM;
                account.Password = _passwordHasher.HashPassword(req.Password);
                await _unitOfWork.Accounts.CreateAsync(account);
                // if (req.RoleName != EnumRoleName.ROLE_CUSTOMER)
                // {
                //     Employee employee = new Employee()
                //     {
                //         RefCode = GenerateRandomNumber(),
                //         // StoreId = req.StoreId.Value,
                //     };
                //     await _unitOfWork.Employees.CreateAsync(employee);
                //     await _unitOfWork.SaveChangesWithTransactionAsync();
                //     account.EmployeeId = employee.Id;
                //     account.Employee = employee;
                // }
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

        public async Task<bool> DeleteAccount(Guid id)
        {
            try
            {
                var account = await _unitOfWork.Accounts.GetByIdAsync(id);
                if (account == null) throw new AppException(ErrorCode.USER_NOT_FOUND);
                await _unitOfWork.Accounts.DeleteAsync(account);
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

        public async Task<IEnumerable<AccountResponse>> GetAllAccounts()
        {
            try
            {
                var accounts = await _unitOfWork.Accounts.FindAll();
                if (accounts == null) throw new AppException(ErrorCode.LIST_EMPTY);
                IEnumerable<AccountResponse> accountResponses = _mapper.Map<IEnumerable<AccountResponse>>(accounts);
                return accountResponses;
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

        public async Task<PageResult<AccountResponse>> GetAllByPaging(int pageCurrent, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.Accounts.FindAll();
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

        public async Task<AccountResponse> GetById(Guid id)
        {
            try
            {
                var account = await _unitOfWork.Accounts.GetByIdAsync(id);
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

        public async Task<PageResult<AccountResponse>> GetWithParams(string? lastName, string? firstName, EnumAccountStatus? status, EnumRoleName? role, int pageCurrent, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.Accounts.FindWithParams(lastName, firstName, status, role);
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

        public async Task<bool> UpdateAccount(UpdateAccountRequest req, Guid id, bool isAdmin)
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

        public async Task<bool> UpdateAccountStatus(Guid id, EnumAccountStatus status, bool isAdmin)
        {
            try
            {
                if (!isAdmin) throw new AppException(ErrorCode.UNAUTHORIZED);
                var account = await _unitOfWork.Accounts.GetByIdAsync(id);
                if (account == null) throw new AppException(ErrorCode.USER_NOT_FOUND);
                account.EnumAccountStatus = status;
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


    }
}
