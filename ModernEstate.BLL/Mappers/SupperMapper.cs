
using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class SupperMapper : Profile
    {
        public SupperMapper()
        {
            CreateMap<SupportRequest, Support>().ReverseMap();
            CreateMap<Support, SupportResponse>().ReverseMap();
        }
    }
}