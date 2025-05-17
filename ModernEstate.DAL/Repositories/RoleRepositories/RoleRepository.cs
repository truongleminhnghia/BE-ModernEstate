
using Microsoft.EntityFrameworkCore;
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Context;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.RoleRepositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbConext context) : base(context) { }

        public async Task<Role?> GetByName(EnumRoleName name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleName == name);
        }
    }
}