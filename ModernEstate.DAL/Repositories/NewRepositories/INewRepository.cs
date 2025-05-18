using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.NewRepositories
{
    public interface INewRepository
    {
        Task<New?> GetByIdAsync(Guid id);
        Task<IEnumerable<New>> GetAllAsync();
        Task<IEnumerable<New>> FindAsync(Expression<Func<New, bool>> predicate);

        Task AddAsync(New entity);
        void Update(New entity);
        void Remove(New entity);

        Task<New?> GetBySlugAsync(string slug);
        Task<IEnumerable<New>> GetByStatusAsync(EnumStatusNew status);
        Task<IEnumerable<New>> GetByPublishDateRangeAsync(DateTime from, DateTime to);
    }
}
