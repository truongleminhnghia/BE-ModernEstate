
using Microsoft.Extensions.Logging;
using ModernEstate.BLL.Services.AccountServices;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.DashBoards;
using ModernEstate.DAL;

namespace ModernEstate.BLL.Services.DashBoardServices
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DashBoardService> _logger;
        private readonly IAccountService _accountServcie;


        public DashBoardService(IUnitOfWork unitOfWork, ILogger<DashBoardService> logger, IAccountService accountService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _accountServcie = accountService;
        }

        public async Task<AccountDashBoard> GetAccountDashBoardAsync()
        {
            try
            {
                DateTime now = DateTime.Now;
                DateTime fromDate = now.AddDays(-7);
                var accountsList = await _unitOfWork.Accounts.FindAll();
                var totalAccounts = accountsList.Count();
                var accounts = await _accountServcie.GetAll(null, null, null, null, null, null, 0, fromDate, now);
                AccountDashBoard account = new AccountDashBoard
                {
                    TotalAccounts = totalAccounts,
                    Accounts = accounts
                };
                return account;
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