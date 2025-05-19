
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AccountServiceRepositories
{
    public class AccountBuyServiceRepository : GenericRepository<AccountBuyService>, IAccountBuyServiceRepository
    {
        public AccountBuyServiceRepository(ApplicationDbConext context) : base(context) { }
    }

}
