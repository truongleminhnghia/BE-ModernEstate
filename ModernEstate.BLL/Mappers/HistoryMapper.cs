
using AutoMapper;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class HistoryMapper : Profile
    {
        public HistoryMapper()
        {
            // CreateMap<ImageRequest, Image>().ReverseMap();
            CreateMap<History, HistoryResponse>().ReverseMap();
        }
    }
}