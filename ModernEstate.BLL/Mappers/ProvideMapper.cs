using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    internal class ProvideMapper : Profile
    {
        public ProvideMapper()
        {
            CreateMap<ProvideRequest, Provide>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Provide, ProvideResponse>();
        }
    }
}
