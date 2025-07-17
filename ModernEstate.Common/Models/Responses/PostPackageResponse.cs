
using System.ComponentModel;
using ModernEstate.Common.Enums;

namespace ModernEstate.Common.Models.Responses
{
    public class PostPackageResponse
    {
        public Guid Id { get; set; }

        [Description("Start date of the subscription")]
        public DateTime StartDate { get; set; }

        [Description("End date of the subscription")]
        public DateTime EndDate { get; set; }

        [Description("Status of the subscription")]
        public EnumStatus Status { get; set; }

        public double TotalAmout { get; set; }

        [Description("Currency of the subscription price")]
        public EnumCurrency Currency { get; set; } = EnumCurrency.VND;

        [Description("Total number of days for the subscription")]
        public int TotalDay { get; set; }

        [Description("Purchase date of the subscription")]
        public DateTime PurchaseDate { get; set; }

        [Description("Expiration date of the subscription")]
        public DateTime ExpiredDate { get; set; }

        [Description("Navigation property for the associated account")]
        public AccountResponse? Account { get; set; }

        [Description("Navigation property for the associated package")]
        public PackageResponse? Package { get; set; }
    }
}