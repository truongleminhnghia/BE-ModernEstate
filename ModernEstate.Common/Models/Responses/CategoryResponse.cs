using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Responses
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public EnumCategoryName CategoryName { get; set; }
    }
}
