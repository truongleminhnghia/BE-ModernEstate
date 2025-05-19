using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
