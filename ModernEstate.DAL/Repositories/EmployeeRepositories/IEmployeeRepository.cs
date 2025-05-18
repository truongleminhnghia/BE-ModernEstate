using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.EmployeeRepositories
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(Guid id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Employee>> FindAsync(Expression<Func<Employee, bool>> predicate);

        Task AddAsync(Employee entity);
        void Update(Employee entity);
        void Remove(Employee entity);
    }
}
