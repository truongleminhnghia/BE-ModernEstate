using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.FavoriteRepositories
{
    public interface IFavoriteRepository
    {
        Task<Favorite?> GetByIdAsync(Guid id);
        Task<IEnumerable<Favorite>> GetAllAsync();
        Task<IEnumerable<Favorite>> FindAsync(Expression<Func<Favorite, bool>> predicate);

        Task AddAsync(Favorite entity);
        void Update(Favorite entity);
        void Remove(Favorite entity);
    }
}
