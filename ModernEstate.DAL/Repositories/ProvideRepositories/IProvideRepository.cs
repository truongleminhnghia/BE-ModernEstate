using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ProvideRepositories
{
    public interface IProvideRepository
    {
        Task<Provide?> GetByIdAsync(Guid id);
        Task<IEnumerable<Provide>> GetAllAsync();
        Task<IEnumerable<Provide>> FindAsync(Expression<Func<Provide, bool>> predicate);

        Task AddAsync(Provide entity);
        void Update(Provide entity);
        void Remove(Provide entity);
    }
}
