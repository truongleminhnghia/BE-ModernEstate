using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AddressRepositories
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbConext context)
            : base(context) { }

        public async Task<Address> GetOrCreateAsync(Address address)
        {
            var existing = await _context.Addresses.FirstOrDefaultAsync(a =>
                a.HouseNumber == address.HouseNumber
                && a.Street == address.Street
                && a.Ward == address.Ward
                && a.District == address.District
                && a.City == address.City
                && a.Country == address.Country
            );
            return existing;
        }

        public async Task<IEnumerable<Address>> FindAddressesAsync(
            string? city,
            string? district,
            string? ward
        )
        {
            IQueryable<Address> q = _context.Addresses;

            if (!string.IsNullOrWhiteSpace(city))
                q = q.Where(a => a.City.Contains(city));

            if (!string.IsNullOrWhiteSpace(district))
                q = q.Where(a => a.District.Contains(district));

            if (!string.IsNullOrWhiteSpace(ward))
                q = q.Where(a => a.Ward.Contains(ward));

            return await q.OrderByDescending(a => a.Id).ToListAsync();
        }
    }
}
