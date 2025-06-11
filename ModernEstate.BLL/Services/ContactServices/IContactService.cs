

using ModernEstate.Common.Models.Requests;

namespace ModernEstate.BLL.Services.ContactServices
{
    public interface IContactService
    {
        Task<Guid> GetOrCreateAsync(ContactRequest request);
    }
}