

using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class PostPackageMapper : Profile
    {
        public PostPackageMapper()
        {
            CreateMap<PostPackageReuqest, PostPackage>().ReverseMap();
            // CreateMap<Post, PostResponse>().ReverseMap();
        }
    }
}