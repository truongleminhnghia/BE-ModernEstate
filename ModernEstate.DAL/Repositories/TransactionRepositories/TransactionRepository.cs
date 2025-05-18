using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.TransactionRepositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbConext _context;
        private readonly DbSet<Transaction> _dbSet;

        public TransactionRepository(ApplicationDbConext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<Transaction>();
        }

        public async Task<Transaction?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<Transaction>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<Transaction>> FindAsync(
            Expression<Func<Transaction, bool>> predicate
        ) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(Transaction entity) => await _dbSet.AddAsync(entity);

        public void Update(Transaction entity) => _dbSet.Update(entity);

        public void Remove(Transaction entity) => _dbSet.Remove(entity);

        public async Task<Transaction?> GetByCodeAsync(string transactionCode) =>
            await _dbSet.SingleOrDefaultAsync(t => t.TransactionCode == transactionCode);

        public async Task<IEnumerable<Transaction>> GetByStatusAsync(EnumStatusPayment status) =>
            await _dbSet.Where(t => t.Status == status).ToListAsync();

        public async Task<IEnumerable<Transaction>> GetByTypeAsync(EnumTypeTransaction type) =>
            await _dbSet.Where(t => t.TypeTransaction == type).ToListAsync();

        public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId) =>
            await _dbSet.Where(t => t.AccountId == accountId).ToListAsync();
    }
}
