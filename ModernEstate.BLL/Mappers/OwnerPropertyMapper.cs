
using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class OwnerPropertyMapper : Profile
    {
        public OwnerPropertyMapper()
        {
            CreateMap<OwnerProperty, OwnerPropertyResponse>().ReverseMap();
            CreateMap<OwnerPropertyRequest, OwnerProperty>().ReverseMap();
        }
    }
}