
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ProjectRepositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<Project?> FindById(Guid id);
        Task<Project?> FindByTitle(string title);
        Task<IEnumerable<Project>> FindProjects(EnumProjectType? projectType, string? title,
                                                                float? minArea, float? maxArea, EnumProjectStatus?
                                                                projectStatus, string? invetorName);
    }
}
