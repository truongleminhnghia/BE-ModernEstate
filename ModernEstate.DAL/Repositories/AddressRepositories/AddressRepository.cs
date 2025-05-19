using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AddressRepositories
{
    public class AddressRepository : GenericRepositoryitory<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbConext context) : base(context) { }

        public async Task<Address?> GetByIdAsync(Guid id)
        {
            return await _context.Addresses.FindAsync(id);
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<IEnumerable<Address>> FindAsync(
            Expression<Func<Address, bool>> predicate
        )
        {
            return await _context.Addresses.Where(predicate).ToListAsync();
        }
    }
    
        
}
