using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ProjectRepositories
{
    public interface IProjectRepository
    {
        Task<Project?> GetByIdAsync(Guid id);
        Task<IEnumerable<Project>> GetAllAsync();
        Task<IEnumerable<Project>> FindAsync(Expression<Func<Project, bool>> predicate);

        Task AddAsync(Project entity);
        void Update(Project entity);
        void Remove(Project entity);
        Task<Project?> GetByCodeAsync(string code);
        Task<IEnumerable<Project>> GetByTypeAsync(EnumProjectType type);
        Task<IEnumerable<Project>> GetByStatusAsync(EnumProjectStatus status);
        Task<IEnumerable<Project>> GetByAreaRangeAsync(float minArea, float maxArea);
    }
}
