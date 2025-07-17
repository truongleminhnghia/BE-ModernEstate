
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.Common.Models.DashBoards
{
    public class PostDashboard
    {
        public int TotalCount { get; set; }
        public int TotalConfirm { get; set; }
        public IEnumerable<PostResponse>? Posts { get; set; }
    }
}