

namespace ModernEstate.Common.Models.Responses
{
    public class OwnerPropertyResponse
    {
        public Guid Id { get; set; }
        public string? Code { get; set; }
        public AccountResponse? Account { get; set; }
    }
}