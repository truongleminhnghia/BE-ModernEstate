
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModernEstate.BLL.Services.AccountServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.DashBoards;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Repositories.TransactionRepositories;

namespace ModernEstate.BLL.Services.DashBoardServices
{
    public class DashBoardService : IDashBoardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DashBoardService> _logger;
        private readonly IAccountService _accountServcie;
        private readonly IMapper _mapper;


        public DashBoardService(IUnitOfWork unitOfWork, ILogger<DashBoardService> logger, IAccountService accountService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _accountServcie = accountService;
            _mapper = mapper;
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

        public async Task<PostDashboard> Post()
        {
            try
            {
                var postList = await _unitOfWork.Posts.FindWithParams(null, null, null, null, null, null);
                var total = postList.Count();
                var postConfirms = await _unitOfWork.Posts.FindWithParams(null, EnumSourceStatus.WAIT_APPROVE, null, null, null, null);
                PostDashboard postDashboard = new PostDashboard
                {
                    TotalCount = total,
                    TotalConfirm = postConfirms.Count(),
                    Posts = _mapper.Map<IEnumerable<PostResponse>>(postList)
                };
                return postDashboard;

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

        public async Task<double> GetTotalAmountAsync()
        {
            var transactions = await _unitOfWork.Transactions.GetAllAsync();
            if (!transactions.Any(t => t.Status == EnumStatusPayment.SUCCESS))
                return 0;
            double total = transactions
                    .Where(t => t.Status == EnumStatusPayment.SUCCESS)
                    .Sum(t => t.Amount);
            return total;
        }

        public async Task<(List<object> RentTrends, List<object> SellTrends)> GetDemandTrendsAsync()
        {
            var query = _unitOfWork.Posts.GetPostsCreatedInLast7Days();

            var data = await query
                .GroupBy(p => p.CreatedAt.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    RentCount = g.Count(p => p.Demand == EnumDemand.CHO_THUÊ),
                    SellCount = g.Count(p => p.Demand == EnumDemand.MUA_BÁN),
                    Total = g.Count()
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            var result = data.Select(x => new PostTrendResponse
            {
                Date = x.Date.ToString("yyyy-MM-dd"),
                RentPercentage = x.Total == 0 ? 0 : Math.Round(100.0 * x.RentCount / x.Total, 2),
                SellPercentage = x.Total == 0 ? 0 : Math.Round(100.0 * x.SellCount / x.Total, 2)
            }).ToList();

            var rentTrends = result.Select(x => new { x.Date, x.RentPercentage }).ToList<object>();
            var sellTrends = result.Select(x => new { x.Date, x.SellPercentage }).ToList<object>();

            return (rentTrends, sellTrends);
        }

    }
}