
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AddressRepositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbConext context) : base(context) { }

        public async Task<Guid> GetOrCreateAsync(Address address)
        {
            var existing = await _context.Addresses
                                        .FirstOrDefaultAsync(a =>
                                        a.HouseNumber == address.HouseNumber &&
                                        a.Street == address.Street &&
                                        a.Ward == address.Ward &&
                                        a.District == address.District &&
                                        a.City == address.City &&
                                        a.Country == address.Country
                                        );
            if (existing != null)
                return existing.Id;

            // 2. Chưa có -> thêm mới
            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();
            return address.Id;
        }
    }


}
