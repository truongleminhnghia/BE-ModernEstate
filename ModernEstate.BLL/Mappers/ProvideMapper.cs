
using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class ProvideMapper : Profile
    {
        public ProvideMapper()
        {
            CreateMap<ProvideRequest, Provide>().ReverseMap();
            CreateMap<Provide, ProvideResponse>().ReverseMap();
        }
    }
}
