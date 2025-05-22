
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
namespace ModernEstate.BLL.Services.AccountServices
{
    public interface IAccountService
    {
        public Task<bool> CreateAccount(AccountRequest req);
        public Task<AccountResponse> GetById(Guid id);
        Task<Guid> CreateAccountBrokerOrOwner(AccountRequest req);
        public Task<AccountResponse> GetByEmail(string email);
        public Task<AccountResponse> GetByPhone(string phone);
        public Task<bool> UpdateAccount(UpdateAccountRequest req, Guid id);
        public Task<bool> UpdateAccountStatus(Guid id);
        Task<PageResult<AccountResponse>> GetWithParams(string? lastName, string? firstName, EnumAccountStatus? status, EnumRoleName? role, EnumGender? gender, string email, int pageCurrent, int pageSize);
    }
}
