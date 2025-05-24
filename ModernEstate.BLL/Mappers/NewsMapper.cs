using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;


namespace ModernEstate.BLL.Mappers
{
    public class NewsMapper : Profile
    {
        public NewsMapper() 
        {
            CreateMap<NewsRequest, New>()
                .ForMember(dest => dest.NewTags, opt => opt.Ignore());
            CreateMap<New, NewsResponse>();
        }
        
    }
}
