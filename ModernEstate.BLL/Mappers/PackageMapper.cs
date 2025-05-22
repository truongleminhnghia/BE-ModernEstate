using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    internal class PackageMapper : Profile
    {
        public PackageMapper()
        {
            CreateMap<PackageRequest, Package>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Package, PackageResponse>();
        }
    }
}
