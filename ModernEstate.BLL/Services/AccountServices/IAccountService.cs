
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
namespace ModernEstate.BLL.Services.AccountServices
{
    public interface IAccountService
    {
        public Task<bool> CreateAccount(AccountRequest req, bool _isAdmin);
        public Task<AccountResponse> GetById(Guid id);
        public Task<IEnumerable<AccountResponse>> GetAllAccounts();
        public Task<bool> UpdateAccount(UpdateAccountRequest req, Guid id, bool isAdmin);
        public Task<bool> UpdateAccountStatus(Guid id, EnumAccountStatus status, bool isAdmin);
        public Task<bool> DeleteAccount(Guid id);
        Task<PageResult<AccountResponse>> GetAllByPaging(int pageCurrent, int pageSize);
        Task<PageResult<AccountResponse>> GetWithParams(string? lastName, string? firstName, EnumAccountStatus? status, EnumRoleName? role, int pageCurrent, int pageSize);
    }
}
