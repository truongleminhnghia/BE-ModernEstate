using AutoMapper;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ShoppEcommerce_WebApp.BLL.Mappers
{
    public class RoleMapper : Profile
    {
        public RoleMapper()
        {
            CreateMap<Role, RoleResponse>();
        }
    }
}
