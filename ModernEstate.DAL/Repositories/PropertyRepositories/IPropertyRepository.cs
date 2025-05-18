using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.PropertyRepositories
{
    public interface IPropertyRepository
    {
        Task<Property?> GetByIdAsync(Guid id);
        Task<IEnumerable<Property>> GetAllAsync();
        Task<IEnumerable<Property>> FindAsync(Expression<Func<Property, bool>> predicate);

        Task AddAsync(Property entity);
        void Update(Property entity);
        void Remove(Property entity);

        Task<Property?> GetByCodeAsync(string code);
        Task<IEnumerable<Property>> GetByStateAsync(EnumStateProperty state);
        Task<IEnumerable<Property>> GetByStatusAsync(EnumStatusProperty status);
        Task<IEnumerable<Property>> GetByTransactionTypeAsync(EnumTypeTransaction type);
        Task<IEnumerable<Property>> GetByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    }
}
