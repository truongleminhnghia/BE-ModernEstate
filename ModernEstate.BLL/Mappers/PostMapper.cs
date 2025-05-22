
using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class PostMapper : Profile
    {
        public PostMapper()
        {
            CreateMap<PostRequest, Post>().ReverseMap();
            CreateMap<Post, PostResponse>().ReverseMap();
        }
    }
}