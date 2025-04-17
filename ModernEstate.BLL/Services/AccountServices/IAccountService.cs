using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernEstate.BLL.Services.AccountServices
{
    public interface IAccountService
    {
        public Task<bool> CreateAccount(AccountRequest req, bool _isAdmin);
        public Task<AccountResponse> GetById(Guid id);
    }
}
