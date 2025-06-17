
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.Common.Models.DashBoards
{
    public class AccountDashBoard
    {
        public int TotalAccounts { get; set; }
        public IEnumerable<AccountResponse>? Accounts { get; set; }

    }
}