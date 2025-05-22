
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AddressRepositories
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<Guid> GetOrCreateAsync(Address address);
    }


}
