using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.CategoryRepositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Category> _dbSet;

        public CategoryRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Category>();
        }

        public async Task<Category?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Category>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Category>> FindAsync(
            Expression<Func<Category, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Category entity) => await _dbSet.AddAsync(entity);

        public void Update(Category entity) => _dbSet.Update(entity);

        public void Remove(Category entity) => _dbSet.Remove(entity);

        public async Task<Category?> GetByNameAsync(EnumCategoryName name) =>
            await _dbSet.SingleOrDefaultAsync(c => c.CategoryName == name);
    }
}
