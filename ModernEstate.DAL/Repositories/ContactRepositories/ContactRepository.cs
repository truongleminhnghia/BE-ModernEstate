using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.ContactRepositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Contact> _dbSet;

        public ContactRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Contact>();
        }

        public async Task<Contact?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Contact>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Contact>> FindAsync(
            Expression<Func<Contact, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Contact entity) => await _dbSet.AddAsync(entity);

        public void Update(Contact entity) => _dbSet.Update(entity);

        public void Remove(Contact entity) => _dbSet.Remove(entity);
    }
}
