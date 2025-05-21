
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.InvetorRepositories
{
    public class InvetorRepository : GenericRepository<Invetor>, IInvetorRepository
    {
        public InvetorRepository(ApplicationDbConext context) : base(context)
        {

        }

        public Task<Invetor?> FindByEmail(string email)
        {
            return _context.Inventories.Include(i => i.Projects)
                                        .FirstOrDefaultAsync(i => i.Email.Equals(email));
        }

        public Task<Invetor?> FindById(Guid id)
        {
            return _context.Inventories.Include(i => i.Projects)
                                        .FirstOrDefaultAsync(i => i.Id.Equals(id));
        }

        public Task<Invetor?> FindByName(string name)
        {
            return _context.Inventories.Include(i => i.Projects)
                                        .FirstOrDefaultAsync(i => i.Name.Equals(name));
        }

        public async Task<IEnumerable<Invetor>> FindInvetors(string? name, string? companyName, EnumInvetorType? invetorType, string email)
        {
            IQueryable<Invetor> query = _context.Inventories.Include(a => a.Projects);

            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(a => a.Name.Contains(name));
            }
            if (!string.IsNullOrWhiteSpace(companyName))
            {
                query = query.Where(a => a.CompanyName.Contains(companyName));
            }
            if (!string.IsNullOrWhiteSpace(email))
            {
                query = query.Where(a => a.Email.Contains(email));
            }
            if (invetorType.HasValue)
            {
                query = query.Where(a => a.InvetorType == invetorType.Value);
            }
            query = query.OrderByDescending(a => a.CreatedAt); // mặc định là giảm dần, tức là cái mới nhất sẽ ở trên cùng
            // Thay đổi thứ tự sắp xếp nếu cần thiết
            return await query.ToListAsync();
        }
    }
}
