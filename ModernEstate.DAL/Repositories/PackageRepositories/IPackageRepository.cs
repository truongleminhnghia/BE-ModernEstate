using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.PackageRepositories
{
    public interface IPackageRepository : IGenericRepository<Package>
    {
        Task<IEnumerable<Package>> FindPackagesAsync(EnumTypePackage? typePackage);
    }
}
