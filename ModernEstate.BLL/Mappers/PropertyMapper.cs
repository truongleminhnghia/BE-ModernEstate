
using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class PropertyMapper : Profile
    {
        public PropertyMapper()
        {
            CreateMap<PropertyRequest, Property>().ReverseMap();
            CreateMap<UpdatePropertyRequest, Property>().ReverseMap();
            CreateMap<Property, PropertyResponse>().ReverseMap();
        }
    }
}