
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Responses
{
    public class RoleResponse
    {
        public Guid Id { get; set; }
        public EnumRoleName RoleName { get; set; }
    }
}