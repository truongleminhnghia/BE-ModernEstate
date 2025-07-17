using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Common.Models.Requests
{
    public class ReviewRequest
    {
        [Required(ErrorMessage = "Account ID là bắt buộc")]
        public Guid AccountId { get; set; }

        public float? Rating { get; set; }
        public string? Comment { get; set; }
    }
}