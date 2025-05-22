
namespace ModernEstate.Common.Models.Responses
{
    public class ContactResponse
    {
        public Guid Id { get; set; }
        public string? ContactName { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
    }
}