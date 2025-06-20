

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Requests
{
    public class PostPackageReuqest
    {
        [Required(ErrorMessage = "Mã gói đăng bài là bắt buộc.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc gói đăng bài là bắt buộc.")]
        [Description("Start date of the subscription")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public double TotalAmout { get; set; }

        [Required]
        [Description("Currency of the subscription price")]
        public EnumCurrency? Currency { get; set; } = EnumCurrency.VND;

        [Description("ID of the account associated with the subscription")]
        public Guid AccountId { get; set; }

        [Required]
        [Description("ID of the package associated with the subscription")]
        public Guid PackageId { get; set; }

    }
}