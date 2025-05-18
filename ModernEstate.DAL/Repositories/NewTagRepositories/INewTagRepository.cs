using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.NewTagRepositories
{
    public interface INewTagRepository
    {
        Task<NewTag?> GetByIdAsync(Guid id);
        Task<IEnumerable<NewTag>> GetAllAsync();
        Task<IEnumerable<NewTag>> FindAsync(Expression<Func<NewTag, bool>> predicate);

        Task AddAsync(NewTag entity);
        void Update(NewTag entity);
        void Remove(NewTag entity);
    }
}
