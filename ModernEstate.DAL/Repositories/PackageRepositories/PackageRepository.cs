using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.PackageRepositories
{
    public class PackageRepository : GenericRepository<Package>, IPackageRepository
    {
        public PackageRepository(ApplicationDbConext context)
            : base(context) { }

        public async Task<IEnumerable<Package>> FindPackagesAsync(EnumTypePackage? typePackage)
        {
            IQueryable<Package> query = _context.Packages;

            if (typePackage.HasValue)
                query = query.Where(p => p.TypePackage == typePackage.Value);

            return await query.OrderByDescending(p => p.Price).ToListAsync();
        }
    }
}
