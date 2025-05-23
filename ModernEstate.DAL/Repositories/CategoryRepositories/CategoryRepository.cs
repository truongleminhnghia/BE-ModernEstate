using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.CategoryRepositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbConext context)
            : base(context) { }

        public async Task<IEnumerable<Category>> FindCategoriesAsync(EnumCategoryName? categoryName)
        {
            IQueryable<Category> query = _context.Categories;

            if (categoryName.HasValue)
                query = query.Where(c => c.CategoryName == categoryName.Value);

            return await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
        }

        public async Task<Category?> FindByNameAsync(string name)
        {
            return await _context.Categories.FirstOrDefaultAsync(c =>
                c.CategoryName.ToString() == name
            );
        }
    }
}
