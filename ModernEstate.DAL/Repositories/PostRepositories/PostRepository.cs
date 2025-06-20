
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
            return await _context.Posts
                                .Include(p => p.Property)
                                    .ThenInclude(pro => pro.Address)
                                .Include(p => p.Contact)
                                .Include(p => p.PostPackages)
                                .Include(p => p.Histories)
                                .FirstOrDefaultAsync(p => p.Code.Equals(code));
        }

        public async Task<IEnumerable<Post>> FindByConfirm(EnumSourceStatus? srcStatus, DateTime? dateNow)
        {
            var today = (dateNow ?? DateTime.Now).Date;
            var baseQuery = _context.Posts
                                    .Include(p => p.Contact)
                                    .Include(p => p.PostPackages)
                                    .Include(p => p.Histories)
                                    .Include(p => p.Property)
                                        .ThenInclude(pro => pro.Address)
                                    .AsQueryable();
            if (srcStatus.HasValue)
            {
                baseQuery = baseQuery.Where(p => p.SourceStatus == srcStatus.Value);
            }
            var post = baseQuery.Select(p => new
            {
                Post = p,
                EarliestStartDate = p.PostPackages.Select(pp => (DateTime?)pp.StartDate).Min()
            });
            var ordered = await post.OrderBy(x =>
                x.EarliestStartDate.HasValue ? (x.EarliestStartDate.Value.Date < today ? 0
                : x.EarliestStartDate.Value.Date == today ? 1
                : 2)
                : 2
            ).ThenBy(x => x.EarliestStartDate).Select(x => x.Post).ToListAsync();
            return ordered;
        }

        public async Task<Post?> FindById(Guid id)
        {
            return await _context.Posts
                                .Include(p => p.Property)
                                    .ThenInclude(pro => pro.Address)
                                .Include(p => p.Contact)
                                .Include(p => p.PostPackages)
                                .Include(p => p.Histories)
                                .FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<Post>> FindWithParams(string? title, EnumStatePost? state, EnumSourceStatus? srcStatus)
        {
            IQueryable<Post> query = _context.Posts
                                .Include(p => p.Property)
                                    .ThenInclude(pro => pro.Address)
                                .Include(p => p.Contact)
                                .Include(p => p.PostPackages)
                                    .ThenInclude(pp => pp.Account)
                                .Include(p => p.Histories);

            // if (!string.IsNullOrWhiteSpace(title))
            // {
            //     query = query.Where(a => a.Title.Contains(title));
            // }
            // if (state.HasValue)
            // {
            //     query = query.Where(a => a.State == state.Value);
            // }
            if (srcStatus.HasValue)
            {
                query = query.Where(a => a.SourceStatus == srcStatus.Value);
            }
            query = query.OrderByDescending(a => a.CreatedAt);
            return await query.ToListAsync();
        }
    }

}
