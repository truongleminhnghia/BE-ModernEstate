

using AutoMapper;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class BrokerMapper : Profile
    {
        public BrokerMapper()
        {
            CreateMap<Broker, BrokerResponse>().ReverseMap();
        }
    }
}