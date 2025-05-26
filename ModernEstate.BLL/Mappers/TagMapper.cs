using AutoMapper;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernEstate.BLL.Mappers
{
    public class TagMapper : Profile
    {
        public TagMapper()
        {
            CreateMap<Tag, TagResponse>();

        }
    }
}
