using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.PostPackageRepositories
{
    public interface IPostPackageRepository
    {
        Task<PostPackage?> GetByIdAsync(Guid id);
        Task<IEnumerable<PostPackage>> GetAllAsync();
        Task<IEnumerable<PostPackage>> FindAsync(Expression<Func<PostPackage, bool>> predicate);

        Task AddAsync(PostPackage entity);
        void Update(PostPackage entity);
        void Remove(PostPackage entity);
        Task<IEnumerable<PostPackage>> GetByAccountIdAsync(Guid accountId);
        Task<IEnumerable<PostPackage>> GetByPackageIdAsync(Guid packageId);
        Task<IEnumerable<PostPackage>> GetByPostIdAsync(Guid postId);
        Task<IEnumerable<PostPackage>> GetByStatusAsync(EnumStatus status);
        Task<IEnumerable<PostPackage>> GetByDateRangeAsync(DateTime from, DateTime to);
    }
}
