using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class NewMapper : Profile
    {
        public NewMapper()
        {
            CreateMap<NewRequest, New>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<New, NewResponse>();
        }
    }
}
