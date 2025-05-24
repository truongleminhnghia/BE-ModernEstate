using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class FavoriteMapper : Profile
    {
        public FavoriteMapper()
        {
            CreateMap<FavoriteRequest, Favorite>().ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Favorite, FavoriteResponse>();
        }
    }
}
