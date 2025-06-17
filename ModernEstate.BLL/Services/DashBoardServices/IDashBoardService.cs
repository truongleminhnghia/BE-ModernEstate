
using ModernEstate.Common.Models.DashBoards;

namespace ModernEstate.BLL.Services.DashBoardServices
{
    public interface IDashBoardService
    {
        /// <summary>
        /// Gets the account dashboard.
        /// </summary>
        /// <returns>Account dashboard containing total accounts and a list of accounts.</returns>
        Task<AccountDashBoard> GetAccountDashBoardAsync();

        /// <summary>
        /// Gets the property dashboard.
        /// </summary>
        /// <returns>Property dashboard containing total properties and a list of properties.</returns>
        // Task<PropertyDashBoard> GetPropertyDashBoardAsync();
    }
}