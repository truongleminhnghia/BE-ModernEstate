
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.NewRepositories
{
    public class NewRepository : GenericRepository<New>, INewRepository
    {
        public NewRepository(ApplicationDbConext context) : base(context)
        {
        }
        public async Task<New> FindByTitle(string title)
        {
            return await _context.News.FirstOrDefaultAsync(t => t.Title == title);
        }

        public async Task<New?> GetByIdWithDetailsAsync(Guid id)
        {
            return await _context.Set<New>()
        .Include(n => n.Account)
        .Include(n => n.Category)
        .Include(n => n.NewTags!)
            .ThenInclude(nt => nt.Tag)
        .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<IEnumerable<New>> FindNewsAsync(string? title, EnumStatusNew? status, EnumCategoryName? categoryName, string? tagNames, DateTime? startDate,
    DateTime? endDate, string sortBy = "title", bool sortDescending = false)
        {
            var query = _context.News
                .Include(n => n.Account)
                .Include(n => n.Category)
                .Include(n => n.NewTags)!.ThenInclude(nt => nt.Tag)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(n => n.Title!.Contains(title));

            if (status.HasValue)
                query = query.Where(n => n.StatusNew == status.Value);

            if (categoryName.HasValue)
                query = query.Where(n => n.Category != null && n.Category.CategoryName == categoryName.Value);

            if (!string.IsNullOrWhiteSpace(tagNames))
            {
                var tags = tagNames.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                query = query.Where(n => n.NewTags!.Any(nt => tags.Contains(nt.Tag!.TagName!)));
            }

            if (startDate.HasValue)
                query = query.Where(n => n.PublishDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(n => n.PublishDate <= endDate.Value);


            switch (sortBy?.ToLower())
            {
                case "title":
                    query = sortDescending ? query.OrderByDescending(n => n.Title) : query.OrderBy(n => n.Title);
                    break;
                case "statusnew":
                    query = sortDescending ? query.OrderByDescending(n => n.StatusNew) : query.OrderBy(n => n.StatusNew);
                    break;
                case "categoryname":
                    query = sortDescending ? query.OrderByDescending(n => n.Category!.CategoryName) : query.OrderBy(n => n.Category!.CategoryName);
                    break;
                case "publishdate":
                    query = sortDescending ? query.OrderByDescending(n => n.PublishDate) : query.OrderBy(n => n.PublishDate);
                    break;
                default:

                    query = query.OrderBy(n => n.Title);
                    break;
            }
            return await query.ToListAsync();
        }

    }
}
