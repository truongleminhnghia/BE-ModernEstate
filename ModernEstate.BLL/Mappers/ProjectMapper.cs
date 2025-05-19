using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    internal class ProjectMapper : Profile
    {
        public ProjectMapper()
        {
            CreateMap<ProjectRequest, Project>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Project, ProjectResponse>();
        }
    }
}
