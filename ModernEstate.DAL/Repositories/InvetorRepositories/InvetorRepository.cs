using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.InvetorRepositories
{
    public class InvetorRepository : IInvetorRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Invetor> _dbSet;

        public InvetorRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Invetor>();
        }

        public async Task<Invetor?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Invetor>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Invetor>> FindAsync(
            Expression<Func<Invetor, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Invetor entity) => await _dbSet.AddAsync(entity);

        public void Update(Invetor entity) => _dbSet.Update(entity);

        public void Remove(Invetor entity) => _dbSet.Remove(entity);

        public async Task<Invetor?> GetByCodeAsync(string code) =>
            await _dbSet.SingleOrDefaultAsync(i => i.Code == code);

        public async Task<Invetor?> GetByTaxCodeAsync(string taxCode) =>
            await _dbSet.SingleOrDefaultAsync(i => i.TaxCode == taxCode);

        public async Task<Invetor?> GetByEmailAsync(string email) =>
            await _dbSet.SingleOrDefaultAsync(i => i.Email == email);
    }
}
