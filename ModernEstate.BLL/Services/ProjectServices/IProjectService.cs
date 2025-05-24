using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Pages;

namespace ModernEstate.BLL.Services.ProjectServices
{
    public interface IProjectService
    {
        Task<bool> SaveProject(ProjectRequest request);
        Task<ProjectResponse> GetById(Guid id);
        Task<PageResult<ProjectResponse>> GetProjects(EnumProjectType? projectType, string? title, float? minArea,
                                                        float? maxArea, EnumProjectStatus? projectStatus, string?
                                                        invetorName, int pageCurrent, int pageSize);
        Task<bool> UpdateProject(Guid id, UpdateProjectRequest request);
    }
}
