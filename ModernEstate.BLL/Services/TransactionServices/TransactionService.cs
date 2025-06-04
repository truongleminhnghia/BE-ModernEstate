using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using ModernEstate.BLL.Services.VnnPayServices;
using ModernEstate.Common.Config;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVNPayService _vnPayService;
        private readonly VNPayConfiguration _vnPayConfig;

        public TransactionService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IVNPayService vnPayService,
            IOptions<VNPayConfiguration> vnPayConfig
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _vnPayService = vnPayService;
            _vnPayConfig = vnPayConfig.Value;
        }

        public async Task<CashPaymentResponse> PayByCashAsync(CashPaymentRequest dto)
        {
            var account = await _unitOfWork.Accounts.GetByIdAsync(dto.AccountId);
            if (account == null)
                throw new KeyNotFoundException("Account không tồn tại");

            var txn = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = dto.AccountId,
                Amount = dto.Amount,
                Currency = Enum.Parse<EnumCurrency>(dto.Currency),
                TypeTransaction = Enum.Parse<EnumTypeTransaction>(dto.TypeTransaction),
                PaymentMethod = Enum.Parse<EnumPaymentMethod>(dto.PaymentMethod),
                Status = EnumStatusPayment.PENDING,
                TransactionCode =
                    $"CASH-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid():N}.Substring(0,6)",
                CreatedAt = DateTime.UtcNow,
            };

            await _unitOfWork.Transactions.CreateAsync(txn);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CashPaymentResponse>(txn);
        }

        public async Task<VNPayPaymentResponse> CreateVNPayPaymentAsync(VNPayPaymentRequest dto)
        {
            var account = await _unitOfWork.Accounts.GetByIdAsync(dto.AccountId);
            if (account == null)
                throw new KeyNotFoundException("Account không tồn tại");
            // Override ReturnUrl từ appsettings.json
            if (string.IsNullOrEmpty(dto.ReturnUrl))
            {
                dto.ReturnUrl = _vnPayConfig.ReturnError;
            }
            else
            {
                dto.ReturnUrl = _vnPayConfig.ReturnSuccessUrl;
            }
            // Validate các trường
            if (dto.Amount <= 0) throw new ArgumentException("Amount must be greater than 0");
            if (string.IsNullOrEmpty(dto.ReturnUrl)) throw new ArgumentException("Return URL is required");
            // Tạo transaction mới
            var txn = new Transaction
            {
                Id = Guid.NewGuid(),
                AccountId = dto.AccountId,
                Amount = dto.Amount,
                Currency = Enum.Parse<EnumCurrency>(dto.Currency),
                TypeTransaction = Enum.Parse<EnumTypeTransaction>(dto.TypeTransaction),
                PaymentMethod = Enum.Parse<EnumPaymentMethod>(dto.PaymentMethod),
                Status = EnumStatusPayment.PENDING,
                TransactionCode =
                    $"VNPAY-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid():N}.Substring(0,6)",
                CreatedAt = DateTime.UtcNow,
            };

            await _unitOfWork.Transactions.CreateAsync(txn);
            await _unitOfWork.SaveChangesAsync();

            // Tạo URL thanh toán VNPay
            var paymentUrl = _vnPayService.CreatePaymentUrl(dto, _vnPayConfig);

            return new VNPayPaymentResponse
            {
                PaymentUrl = paymentUrl,
                TransactionId = txn.Id,
                TransactionCode = txn.TransactionCode,
                Amount = txn.Amount,
                Currency = txn.Currency.ToString(),
                Status = txn.Status.ToString(),
            };
        }

        public async Task<VNPayCallbackResponse> ProcessVNPayCallbackAsync(Dictionary<string, string> vnpayData)
        {
            // Validate chữ ký
            if (!_vnPayService.ValidateCallback(vnpayData, _vnPayConfig.HashSecret))
            {
                return new VNPayCallbackResponse
                {
                    Success = false,
                    Message = "Invalid callback signature",
                };
            }
            var vnpTxnRef = vnpayData.GetValueOrDefault("vnp_TxnRef");
            var vnpResponseCode = vnpayData.GetValueOrDefault("vnp_ResponseCode");
            var vnpTransactionStatus = vnpayData.GetValueOrDefault("vnp_TransactionStatus");
            if (string.IsNullOrEmpty(vnpTxnRef))
            {
                return new VNPayCallbackResponse
                {
                    Success = false,
                    Message = "Missing transaction reference",
                };
            }
            // Tìm transaction PENDING tương ứng
            var transactions = await _unitOfWork.Transactions.FindTransactionsAsync(null, null, EnumStatusPayment.PENDING, EnumPaymentMethod.VN_PAY);
            var transaction = transactions.FirstOrDefault(t => t.TransactionCode.Contains(vnpTxnRef) || t.Id.ToString() == vnpTxnRef);
            if (transaction == null)
            {
                return new VNPayCallbackResponse
                {
                    Success = false,
                    Message = "Transaction not found",
                };
            }

            // Cập nhật trạng thái
            transaction.Status =
                (vnpResponseCode == "00" && vnpTransactionStatus == "00")
                    ? EnumStatusPayment.SUCCESS
                    : EnumStatusPayment.FAILED;

            await _unitOfWork.Transactions.UpdateAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            return new VNPayCallbackResponse
            {
                TransactionId = transaction.Id,
                TransactionCode = transaction.TransactionCode,
                Success = (transaction.Status == EnumStatusPayment.SUCCESS),
                Message =
                    transaction.Status == EnumStatusPayment.SUCCESS
                        ? "Payment successful"
                        : "Payment failed",
            };
        }

        public async Task<CashPaymentResponse?> GetByIdAsync(Guid id)
        {
            var transaction = await _unitOfWork.Transactions.GetByIdAsync(id);
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
            var all = await _unitOfWork.Transactions.FindTransactionsAsync(
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
