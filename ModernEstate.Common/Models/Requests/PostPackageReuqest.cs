

namespace ModernEstate.Common.Models.Requests
{
    public class PostPackageReuqest
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid? AccountId { get; set; }
        public Guid? PackageId { get; set; }
        public Guid? PostId { get; set; }
    }
}