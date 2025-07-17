using ModernEstate.Common.Models.Responses;

namespace ModernEstate.Common.Models.DashBoards
{
    public class ReviewResponseDashboard
    {
        public int TotalReviews { get; set; }
        public IEnumerable<ReviewResponse>? Reviews { get; set; }
    }
}