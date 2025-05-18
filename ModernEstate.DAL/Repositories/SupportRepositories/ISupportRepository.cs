using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.SupportRepositories
{
    public interface ISupportRepository
    {
        Task<Support?> GetByIdAsync(Guid id);
        Task<IEnumerable<Support>> GetAllAsync();
        Task<IEnumerable<Support>> FindAsync(Expression<Func<Support, bool>> predicate);

        Task AddAsync(Support entity);
        void Update(Support entity);
        void Remove(Support entity);
    }
}
