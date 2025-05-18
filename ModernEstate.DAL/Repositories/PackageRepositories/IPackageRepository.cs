using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.PackageRepositories
{
    public interface IPackageRepository
    {
        Task<Package?> GetByIdAsync(Guid id);
        Task<IEnumerable<Package>> GetAllAsync();
        Task<IEnumerable<Package>> FindAsync(Expression<Func<Package, bool>> predicate);

        Task AddAsync(Package entity);
        void Update(Package entity);
        void Remove(Package entity);
        Task<Package?> GetByCodeAsync(string packageCode);
        Task<IEnumerable<Package>> GetByTypeAsync(EnumTypePackage type);
        Task<IEnumerable<Package>> GetByAccessPriorityAsync(EnumAccessPriority priority);
    }
}
