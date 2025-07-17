
using System.ComponentModel.DataAnnotations;

namespace ModernEstate.Common.Models.Requests
{
    public class AddressRequest
    {
        [Required(ErrorMessage = "Tên Phường ko được thiếu")]
        public string? Ward { get; set; }

        [Required(ErrorMessage = "Tên Quận ko được thiếu")]
        public string? District { get; set; }

        [Required(ErrorMessage = "Tên Thành Phố ko được thiếu")]
        public string? City { get; set; }
        public string? Country { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ cụ thể")]
        public string? AddressDetail { get; set; }
    }
}
