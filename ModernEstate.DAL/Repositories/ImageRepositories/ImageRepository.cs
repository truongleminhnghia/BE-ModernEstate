using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ImageRepositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Image> _dbSet;

        public ImageRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Image>();
        }

        public async Task<Image?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Image>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Image>> FindAsync(Expression<Func<Image, bool>> predicate) =>
            await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Image entity) => await _dbSet.AddAsync(entity);

        public void Update(Image entity) => _dbSet.Update(entity);

        public void Remove(Image entity) => _dbSet.Remove(entity);

        // Ví dụ thêm phương thức đặc thù:
        // public async Task<IEnumerable<Image>> GetByPropertyIdAsync(Guid propertyId) =>
        //     await _dbSet.Where(i => i.PropertyId == propertyId).ToListAsync();
        //
        // public async Task<IEnumerable<Image>> GetByProjectIdAsync(Guid projectId) =>
        //     await _dbSet.Where(i => i.ProjectId == projectId).ToListAsync();
    }
}
