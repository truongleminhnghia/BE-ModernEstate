using AutoMapper;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<CashPaymentResponse> PayByCashAsync(CashPaymentRequest dto)
        {
            var account = await _unitofwork.Accounts.GetByIdAsync(dto.AccountId);
            if (account == null)
                throw new KeyNotFoundException("Account không tồn tại");

            var txn = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = dto.AccountId,
                Amount = dto.Amount,
                Currency = Enum.Parse<EnumCurrency>(dto.Currency),
                TypeTransaction = EnumTypeTransaction.CashIn,
                PaymentMethod = EnumPaymentMethod.CASH,
                Status = EnumStatusPayment.SUCCESS,
                TransactionCode =
                    $"CASH-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 6)}",
                CreatedAt = DateTime.UtcNow,
            };

            await _unitofwork.Transactions.CreateAsync(txn);

            await _unitofwork.SaveChangesAsync();

            return _mapper.Map<CashPaymentResponse>(txn);
        }

        public async Task<CashPaymentResponse?> GetByIdAsync(Guid id)
        {
            var transaction = await _unitofwork.Transactions.GetByIdAsync(id);
            if (transaction == null)
                return null;

            return _mapper.Map<CashPaymentResponse>(transaction);
        }

        public async Task<PageResult<CashPaymentResponse>> GetWithParamsAsync(
            Guid? accountId,
            EnumTypeTransaction? typeTransaction,
            EnumStatusPayment? status,
            EnumPaymentMethod? paymentMethod,
            int pageCurrent,
            int pageSize
        )
        {
            var all = await _unitofwork.Transactions.FindTransactionsAsync(
                accountId,
                typeTransaction,
                status,
                paymentMethod
            );

            if (!all.Any())
                throw new AppException(ErrorCode.LIST_EMPTY);

            var paged = all.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();

            var dtos = _mapper.Map<List<CashPaymentResponse>>(paged);
            return new PageResult<CashPaymentResponse>(dtos, pageSize, pageCurrent, all.Count());
        }
    }
}
