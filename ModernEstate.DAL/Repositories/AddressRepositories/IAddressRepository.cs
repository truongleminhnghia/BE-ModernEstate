using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AddressRepositories
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<Address?> GetByIdAsync(Guid id);
        Task<IEnumerable<Address>> GetAllAsync();
        Task<IEnumerable<Address>> FindAsync(
            Expression<Func<Address, bool>> predicate
        );
    }

    
}
