using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.TagRepositories
{
    public interface ITagRepository
    {
        Task<Tag?> GetByIdAsync(Guid id);
        Task<IEnumerable<Tag>> GetAllAsync();
        Task<IEnumerable<Tag>> FindAsync(Expression<Func<Tag, bool>> predicate);

        Task AddAsync(Tag entity);
        void Update(Tag entity);
        void Remove(Tag entity);
        Task<Tag?> GetByNameAsync(string tagName);
    }
}
