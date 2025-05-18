using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ServiceRepositories
{
    public interface IServiceRepository
    {
        Task<Service?> GetByIdAsync(Guid id);
        Task<IEnumerable<Service>> GetAllAsync();
        Task<IEnumerable<Service>> FindAsync(Expression<Func<Service, bool>> predicate);

        Task AddAsync(Service entity);
        void Update(Service entity);
        void Remove(Service entity);

        Task<Service?> GetByTitleAsync(string title);
        Task<IEnumerable<Service>> GetByTypeAsync(EnumTypeService type);
        Task<IEnumerable<Service>> GetByStatusAsync(EnumStatus status);
        Task<IEnumerable<Service>> GetByProvideIdAsync(Guid provideId);
    }
}
