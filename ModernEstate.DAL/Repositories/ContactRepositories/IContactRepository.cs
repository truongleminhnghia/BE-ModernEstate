
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ContactRepositories
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        Task<Contact?> FindByEmail(string contactEmail);
        Task<Contact?> FindByPhone(string phone);
    }
}
