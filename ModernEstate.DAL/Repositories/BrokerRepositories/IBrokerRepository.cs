using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.BrokerRepositories
{
    public interface IBrokerRepository
    {
        Task<Broker?> GetByIdAsync(Guid id);
        Task<IEnumerable<Broker>> GetAllAsync();
        Task<IEnumerable<Broker>> FindAsync(Expression<Func<Broker, bool>> predicate);

        Task AddAsync(Broker entity);
        void Update(Broker entity);
        void Remove(Broker entity);
    }
}
