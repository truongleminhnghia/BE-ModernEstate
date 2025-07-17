using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.ReviewServices
{
    public interface IReviewService
    {
        Task<bool> CreateReview(ReviewRequest request);
        Task<ReviewResponse?> GetById(Guid id);
        Task<PageResult<ReviewResponse>> GetReviews(Guid? accountId, float? fromRating, float? toRating, string? comment, int pageCurrent, int pageSize);
    }
}