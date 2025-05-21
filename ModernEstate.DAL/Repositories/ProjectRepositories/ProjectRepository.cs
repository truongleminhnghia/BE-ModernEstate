
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ProjectRepositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbConext context) : base(context) { }

        public Task<Project?> FindById(Guid id)
        {
            return _context.Projects.Include(p => p.Address)
                                    .Include(p => p.Invetor)
                                    .FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public Task<Project?> FindByTitle(string title)
        {
            return _context.Projects.Include(p => p.Address)
                                    .Include(p => p.Invetor)
                                    .Include(p => p.Properties)
                                    .Include(p => p.Histories)
                                    .Include(p => p.Images)
                                    .FirstOrDefaultAsync(p => p.Title.Equals(title));
        }

        public async Task<IEnumerable<Project>> FindProjects(
                                                EnumProjectType? projectType,
                                                string? title,
                                                float? maxArea,
                                                float? minArea,
                                                EnumProjectStatus? projectStatus,
                                                string? invetorName)
        {
            IQueryable<Project> query = _context.Projects
                .Include(p => p.Address)
                .Include(p => p.Invetor)
                .Include(p => p.Properties)
                .Include(p => p.Histories)
                .Include(p => p.Images);

            if (projectType.HasValue)
                query = query.Where(p => p.TypeProject == projectType.Value);

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(p => EF.Functions.Like(p.Title, $"%{title}%"));

            if (minArea.HasValue)
                query = query.Where(p => p.ProjectArea >= minArea.Value);

            if (maxArea.HasValue)
                query = query.Where(p => p.ProjectArea <= maxArea.Value);

            if (projectStatus.HasValue)
                query = query.Where(p => p.Status == projectStatus.Value);

            if (!string.IsNullOrWhiteSpace(invetorName))
                query = query.Where(p => EF.Functions.Like(p.Invetor.Name, $"%{invetorName}%"));
            query = query.OrderByDescending(p => p.CreatedAt);
            return await query.ToListAsync();
        }
    }

}
