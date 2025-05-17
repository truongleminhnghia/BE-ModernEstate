
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.RoleRepositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role?> GetByName(EnumRoleName name);
    }
}