using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.CategoryRepositories
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(Guid id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<IEnumerable<Category>> FindAsync(Expression<Func<Category, bool>> predicate);

        Task AddAsync(Category entity);
        void Update(Category entity);
        void Remove(Category entity);

        // Phương thức đặc thù: lấy theo tên enum
        Task<Category?> GetByNameAsync(EnumCategoryName name);
    }
}
