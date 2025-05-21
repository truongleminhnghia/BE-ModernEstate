
using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class InvetorMapper : Profile
    {
        public InvetorMapper()
        {
            CreateMap<InvetorRequest, Invetor>().ReverseMap();
            CreateMap<Invetor, InvetorResponse>().ReverseMap();
        }
    }
}