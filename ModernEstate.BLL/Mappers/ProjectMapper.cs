
using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class ProjectMapper : Profile
    {
        public ProjectMapper()
        {
            CreateMap<ProjectRequest, Project>().ReverseMap();
            CreateMap<Project, ProjectResponse>().ReverseMap();
        }
    }
}