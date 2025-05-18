using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.EmployeeRepositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Employee> _dbSet;

        public EmployeeRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Employee>();
        }

        public async Task<Employee?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Employee>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Employee>> FindAsync(
            Expression<Func<Employee, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Employee entity) => await _dbSet.AddAsync(entity);

        public void Update(Employee entity) => _dbSet.Update(entity);

        public void Remove(Employee entity) => _dbSet.Remove(entity);
    }
}
