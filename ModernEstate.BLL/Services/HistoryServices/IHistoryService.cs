
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.HistoryServices
{
    public interface IHistoryService
    {
        Task<History> CreateHistory(History history);
        Task<History?> GetById(Guid id);
        Task<IEnumerable<History>> GetByProejct(Guid id);
        Task<IEnumerable<History>> GetByProperty(Guid id);
    }
}