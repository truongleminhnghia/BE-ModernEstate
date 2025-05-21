
using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class ContactMapper : Profile
    {
        public ContactMapper()
        {
            CreateMap<ContactRequest, Contact>().ReverseMap();
            CreateMap<Contact, ContactResponse>().ReverseMap();
        }
    }
}