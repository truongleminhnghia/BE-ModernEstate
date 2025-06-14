
using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.srcs;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;
using Net.payOS;
using Net.payOS.Types;

namespace ModernEstate.BLL.Services.PayosServices
{
    public class PayosService : IPayosService
    {
        private readonly ILogger<PayosService> _logger;
        private readonly IUnitOfWork _uow;
        private readonly Utils _utils;
        private readonly IMapper _mapper;
        private readonly PayOS _payOS;

        public PayosService(IUnitOfWork uow, IMapper mapper, ILogger<PayosService> logger,
                            Utils utils, PayOS payOS)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
            _utils = utils;
            _payOS = payOS;
        }

        public async Task<string> CreatePaymentAsync(PostPackageReuqest request)
        {
            try
            {
                if (request.Id == null)
                    throw new AppException(ErrorCode.NOT_NULL, "Id không được null.");
                var postPackageExists = await _uow.PostPackages.FindById(request.Id.Value);
                if (postPackageExists == null)
                {
                    _logger.LogWarning("Post package with ID {id} does not exist.", request.Id);
                    throw new AppException(ErrorCode.NOT_FOUND, "Gói đăng bài không tồn tại.");
                }
                if (request.PackageId == null)
                    throw new AppException(ErrorCode.NOT_NULL, "PackageId không được null.");
                var packageExists = await _uow.Packages.GetByIdAsync(request.PackageId.Value);
                if (packageExists == null)
                {
                    _logger.LogWarning("Package with ID {PackageId} does not exist.", request.PackageId);
                    throw new AppException(ErrorCode.NOT_FOUND, "Gói đăng bài không tồn tại.");
                }
                var totalDays = (request.EndDate - request.StartDate)?.Days + 1;
                if (totalDays == null)
                    throw new AppException(ErrorCode.NOT_NULL, "StartDate hoặc EndDate không được null.");
                postPackageExists.TotalDay = totalDays.Value;
                postPackageExists.PurchaseDate = DateTime.Now;
                postPackageExists.ExpiredDate = request.EndDate;
                postPackageExists.PackageId = request.PackageId;
                await _uow.PostPackages.UpdateAsync(postPackageExists);
                PaymentRequest paymentRequest = new PaymentRequest
                {
                    Amount = request.TotalAmout ?? 0,
                    Currency = request.Currency ?? EnumCurrency.VND,
                    TypeTransaction = EnumTypeTransaction.PACKAGE_PURCHASE,
                    PaymentMethod = EnumPaymentMethod.PayOS,
                    AccountId = postPackageExists.AccountId ?? Guid.Empty
                };
                var payment = _mapper.Map<DAL.Entites.Transaction>(paymentRequest);
                payment.Status = EnumStatusPayment.PENDING;
                payment.PostPackageId = postPackageExists.Id;
                await _uow.Transactions.CreateAsync(payment);
                await _uow.SaveChangesWithTransactionAsync();

                ItemData item = new ItemData(packageExists.PackageName, 1, (int)(request.TotalAmout ?? 0));
                List<ItemData> items = new List<ItemData> { item };
                // if (postPackageExists.Post == null)
                // {
                //     _logger.LogWarning("Post is null for PostPackage with ID {id}.", request.Id);
                //     throw new AppException(ErrorCode.NOT_FOUND, "Bài đăng không tồn tại.");
                // }

                // if (!long.TryParse(postPackageExists.Post.Code, out long postCodeLong))
                // {
                //     _logger.LogWarning("Post.Code '{PostCode}' cannot be converted to long.", postPackageExists.Post.Code);
                //     throw new AppException(ErrorCode.NOT_FOUND, "Mã bài đăng không hợp lệ.");
                // }
                int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));

                PaymentData paymentData = new PaymentData(
                    orderCode,
                    (int)(request.TotalAmout ?? 0),
                    "Thanh toan don hang",
                    items,
                    $"https://localhost:8080/api/v1/checkout/failed",
                    $"https://localhost:8080/api/v1/checkout/success"
                );
                CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);
                return createPayment.checkoutUrl;
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
    }
}