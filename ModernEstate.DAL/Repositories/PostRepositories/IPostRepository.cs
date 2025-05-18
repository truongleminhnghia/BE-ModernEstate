using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.PostRepositories
{
    public interface IPostRepository
    {
        Task<Post?> GetByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<IEnumerable<Post>> FindAsync(Expression<Func<Post, bool>> predicate);

        Task AddAsync(Post entity);
        void Update(Post entity);
        void Remove(Post entity);

        Task<Post?> GetByCodeAsync(string code);
        Task<IEnumerable<Post>> GetByStateAsync(EnumStatePost state);
        Task<IEnumerable<Post>> GetBySourceStatusAsync(EnumSourceStatus sourceStatus);
        Task<IEnumerable<Post>> GetByPropertyIdAsync(Guid propertyId);
    }
}
