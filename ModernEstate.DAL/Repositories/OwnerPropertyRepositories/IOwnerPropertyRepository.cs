using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.OwnerPropertyRepositories
{
    public interface IOwnerPropertyRepository
    {
        Task<OwnerProperty?> GetByIdAsync(Guid id);
        Task<IEnumerable<OwnerProperty>> GetAllAsync();
        Task<IEnumerable<OwnerProperty>> FindAsync(Expression<Func<OwnerProperty, bool>> predicate);

        Task AddAsync(OwnerProperty entity);
        void Update(OwnerProperty entity);
        void Remove(OwnerProperty entity);
    }
}
