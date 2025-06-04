using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.PostRepositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbConext context) : base(context) { }


        public async Task<Post?> FindByCode(string code)
        {
            return await _context.Posts.Include(p => p.Property)
                                .Include(p => p.Contact)
                                .Include(p => p.PostPackages)
                                .Include(p => p.Histories)
                                .FirstOrDefaultAsync(p => p.Code.Equals(code));
        }

        public async Task<Post?> FindById(Guid id)
        {
            return await _context.Posts.Include(p => p.Property)
                                .Include(p => p.Contact)
                                .Include(p => p.PostPackages)
                                .Include(p => p.Histories)
                                .FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<Post>> FindWithParams(string? title, EnumStatePost? state, EnumSourceStatus? srcStatus)
        {
            IQueryable<Post> query = _context.Posts.Include(p => p.Property)
                                .Include(p => p.Contact)
                                .Include(p => p.PostPackages)
                                .Include(p => p.Histories);

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(a => a.Title.Contains(title));
            }
            if (state.HasValue)
            {
                query = query.Where(a => a.State == state.Value);
            }
            if (srcStatus.HasValue)
            {
                query = query.Where(a => a.SourceStatus == srcStatus.Value);
            }
            query = query.OrderByDescending(a => a.CreatedAt);
            return await query.ToListAsync();
        }
    }

}
