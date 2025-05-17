
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.Roles
{
    public interface IRoleService
    {
        Task<bool> Create(EnumRoleName name);
        Task<bool> Delete(Guid id);
        Task<bool> Edit(Guid id, string name);
        Task<RoleResponse> GetRole(Guid id);
        Task<RoleResponse> GetByName(EnumRoleName name);
    }
}