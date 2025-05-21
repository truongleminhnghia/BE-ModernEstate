
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.InvetorServices
{
    public interface IInvetorService
    {
        public Task<bool> CreateInventor(InvetorRequest req);
        public Task<InvetorResponse> GetById(Guid id);
        public Task<bool> UpdateAccount(UpdateAccountRequest req, Guid id);
        public Task<bool> UpdateAccountStatus(Guid id);
        Task<PageResult<InvetorResponse>> GetWithParams(string? name, string? companyName,
                                                        EnumInvetorType? InvetorType, string email,
                                                        int pageCurrent, int pageSize);
    }
}