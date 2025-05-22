
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;
using System;

namespace ModernEstate.DAL.Repositories.BrokerRepositories
{
    public class BrokerRepository : GenericRepository<Broker>, IBrokerRepository
    {
        
        public BrokerRepository(ApplicationDbConext context) : base(context) { }

        public async Task<bool> AnyAsync(Func<Broker, bool> predicate)
        {
            return await Task.FromResult(_context.Brokers.Any(predicate));
        }
    }
}
