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
    public interface IContactRepository
    {
        Task<Contact?> GetByIdAsync(Guid id);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<IEnumerable<Contact>> FindAsync(Expression<Func<Contact, bool>> predicate);

        Task AddAsync(Contact entity);
        void Update(Contact entity);
        void Remove(Contact entity);
    }
}
