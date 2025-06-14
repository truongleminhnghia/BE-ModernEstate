

using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class PostPackageMapper : Profile
    {
        public PostPackageMapper()
        {
            CreateMap<PostPackageReuqest, PostPackage>().ReverseMap();
            CreateMap<PostPackage, PostPackageResponse>().ReverseMap();
        }
    }
}