using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.ProjectServices
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponse>> GetAllAsync();
        Task<ProjectResponse?> GetByIdAsync(Guid id);
        Task<ProjectResponse> CreateAsync(ProjectRequest request);
        Task<bool> UpdateAsync(Guid id, ProjectRequest request);
        Task<bool> DeleteAsync(Guid id);
    }
}
