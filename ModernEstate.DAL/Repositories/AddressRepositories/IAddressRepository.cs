using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AddressRepositories
{
    public interface IAddressRepository
    {
        Task<Address?> GetByIdAsync(Guid id);
        Task<IEnumerable<Address>> GetAllAsync();
        Task<IEnumerable<Address>> FindAsync(Expression<Func<Address, bool>> predicate);

        Task AddAsync(Address entity);
        void Update(Address entity);
        void Remove(Address entity);
    }
}
