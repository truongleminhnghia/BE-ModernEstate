﻿using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.AddressRepositories
{
    public interface IAddressRepository : IGenericRepository<Address>
    {
        Task<Address> GetOrCreateAsync(Address address);
        Task<IEnumerable<Address>> FindAddressesAsync(string? city, string? district, string? ward);
    }
}
