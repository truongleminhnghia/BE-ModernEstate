using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.InvetorRepositories
{
    public interface IInvetorRepository
    {
        Task<Invetor?> GetByIdAsync(Guid id);
        Task<IEnumerable<Invetor>> GetAllAsync();
        Task<IEnumerable<Invetor>> FindAsync(Expression<Func<Invetor, bool>> predicate);

        Task AddAsync(Invetor entity);
        void Update(Invetor entity);
        void Remove(Invetor entity);

        Task<Invetor?> GetByCodeAsync(string code);
        Task<Invetor?> GetByTaxCodeAsync(string taxCode);
        Task<Invetor?> GetByEmailAsync(string email);
    }
}
