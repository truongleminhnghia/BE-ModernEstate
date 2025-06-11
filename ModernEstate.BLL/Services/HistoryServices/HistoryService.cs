

using Microsoft.Extensions.Logging;
using ModernEstate.Common.Exceptions;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.HistoryServices
{
    public class HistoryService : IHistoryService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<HistoryService> _logger;

        public HistoryService(IUnitOfWork unitOfWork, ILogger<HistoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<History> CreateHistory(History history)
        {
            try
            {
                history.ChangeDate = DateTime.UtcNow;
                await _unitOfWork.Histories.CreateAsync(history);
                // await _unitOfWork.SaveChangesWithTransactionAsync();
                return history;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public Task<History?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<History>> GetByProejct(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<History>> GetByProperty(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}