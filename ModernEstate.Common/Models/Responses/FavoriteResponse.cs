using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernEstate.Common.Models.Responses
{
    public class FavoriteResponse
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid PropertyId { get; set; }
    }
}
