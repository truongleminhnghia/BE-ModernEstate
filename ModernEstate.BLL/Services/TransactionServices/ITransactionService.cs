using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;

namespace ModernEstate.BLL.Services.TransactionServices
{
    public interface ITransactionService
    {
        Task<CashPaymentResponse> PayByCashAsync(CashPaymentRequest dto);
        Task<VNPayPaymentResponse> CreateVNPayPaymentAsync(VNPayPaymentRequest dto);
        Task<VNPayCallbackResponse> ProcessVNPayCallbackAsync(Dictionary<string, string> vnpayData);
        Task<CashPaymentResponse?> GetByIdAsync(Guid id);
        Task<PageResult<CashPaymentResponse>> GetWithParamsAsync(
            Guid? accountId,
            EnumTypeTransaction? typeTransaction,
            EnumStatusPayment? status,
            EnumPaymentMethod? paymentMethod,
            int pageCurrent,
            int pageSize
        );
    }
}
