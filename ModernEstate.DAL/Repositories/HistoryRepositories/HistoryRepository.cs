using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.HistoryRepositories
{
    public interface IHistoryRepository
    {
        Task<History?> GetByIdAsync(Guid id);
        Task<IEnumerable<History>> GetAllAsync();
        Task<IEnumerable<History>> FindAsync(Expression<Func<History, bool>> predicate);

        Task AddAsync(History entity);
        void Update(History entity);
        void Remove(History entity);
    }
}
