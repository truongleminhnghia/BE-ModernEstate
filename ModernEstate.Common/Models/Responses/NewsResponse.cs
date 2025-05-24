using ModernEstate.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernEstate.Common.Models.Responses
{
    public class NewsResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Slug { get; set; }
        public EnumStatusNew StatusNew { get; set; }
        public string? Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime PublishDate { get; set; }

        public Guid? AccountId { get; set; }
        public AccountResponse? Account { get; set; }      // full Account object

        public Guid CategoryId { get; set; }

    }
}
