using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.CategoryRepositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<Category?> FindByNameAsync(string name);
        Task<IEnumerable<Category>> FindCategoriesAsync(EnumCategoryName? categoryName);
    }
}
