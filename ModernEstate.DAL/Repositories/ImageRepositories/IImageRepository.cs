using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ImageRepositories
{
    public interface IImageRepository
    {
        Task<Image?> GetByIdAsync(Guid id);
        Task<IEnumerable<Image>> GetAllAsync();
        Task<IEnumerable<Image>> FindAsync(Expression<Func<Image, bool>> predicate);

        Task AddAsync(Image entity);
        void Update(Image entity);
        void Remove(Image entity);
    }
}
