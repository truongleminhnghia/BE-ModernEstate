
using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Common.Models.Requests
{
    public class ChangePasswordRequest
    {
        [Required]
        public string? OldPassword { get; set; }

        [Required]
        public string? NewPassword { get; set; }
    }
}