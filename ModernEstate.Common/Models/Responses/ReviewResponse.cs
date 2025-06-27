namespace ModernEstate.Common.Models.Responses
{
    public class ReviewResponse
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public AccountResponse? Account { get; set; }
        public float? Rating { get; set; }
        public string? Comment { get; set; }
    }
}