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

namespace ModernEstate.DAL.Repositories.ProjectRepositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Project> _dbSet;

        public ProjectRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Project>();
        }

        public async Task<Project?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Project>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Project>> FindAsync(
            Expression<Func<Project, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Project entity) => await _dbSet.AddAsync(entity);

        public void Update(Project entity) => _dbSet.Update(entity);

        public void Remove(Project entity) => _dbSet.Remove(entity);

        public async Task<Project?> GetByCodeAsync(string code) =>
            await _dbSet.SingleOrDefaultAsync(p => p.Code == code);

        public async Task<IEnumerable<Project>> GetByTypeAsync(EnumProjectType type) =>
            await _dbSet.Where(p => p.TypeProject == type).ToListAsync();

        public async Task<IEnumerable<Project>> GetByStatusAsync(EnumProjectStatus status) =>
            await _dbSet.Where(p => p.Status == status).ToListAsync();

        public async Task<IEnumerable<Project>> GetByAreaRangeAsync(float minArea, float maxArea) =>
            await _dbSet
                .Where(p => p.ProjectArea >= minArea && p.ProjectArea <= maxArea)
                .ToListAsync();
    }
}
