
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.PostRepositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<Post?> FindByCode(string code);
        Task<Post?> FindById(Guid id);

        Task<IEnumerable<Post>> FindWithParams(EnumDemand? demand, EnumSourceStatus? srcStatus, Guid? postBy,
                                                EnumStatus? status, Guid? approveBy, EnumPriorityStatus? priority);
        Task<IEnumerable<Post>> FindByConfirm(EnumSourceStatus? srcStatus, DateTime? dateNow);
    }

}
