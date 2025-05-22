using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class CategoryRequest
    {
        public EnumCategoryName CategoryName { get; set; }
    }
}
