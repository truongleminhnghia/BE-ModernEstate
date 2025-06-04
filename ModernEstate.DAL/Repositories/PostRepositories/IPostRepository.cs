
using ModernEstate.Common.Enums;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.DAL.Repositories.PostRepositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<Post?> FindByCode(string code);
        Task<Post?> FindById(Guid id);

        Task<IEnumerable<Post>> FindWithParams(string? title, EnumStatePost? state, EnumSourceStatus? srcStatus);
    }

}
