
using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.srcs;
using ModernEstate.DAL;
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

        public async Task<string> CreatePaymentAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    throw new AppException(ErrorCode.NOT_NULL, "Id không được null.");
                var postPackageExists = await _uow.PostPackages.FindById(id);
                if (postPackageExists == null)
                {
                    _logger.LogWarning("Post package with ID {id} does not exist.", id);
                    throw new AppException(ErrorCode.NOT_FOUND, "Gói đăng bài không tồn tại.");
                }
                if (postPackageExists.PackageId == null)
                    throw new AppException(ErrorCode.NOT_NULL, "PackageId không được null.");
                var packageExists = await _uow.Packages.GetByIdAsync(postPackageExists.PackageId.Value);
                if (packageExists == null)
                {
                    _logger.LogWarning("Package with ID {PackageId} does not exist.", postPackageExists.PackageId);
                    throw new AppException(ErrorCode.NOT_FOUND, "Gói đăng bài không tồn tại.");
                }
                var totalDays = (postPackageExists.EndDate - postPackageExists.StartDate)?.Days + 1;
                if (totalDays == null)
                    throw new AppException(ErrorCode.NOT_NULL, "StartDate hoặc EndDate không được null.");
                postPackageExists.TotalDay = totalDays.Value;
                postPackageExists.PurchaseDate = DateTime.Now;
                postPackageExists.ExpiredDate = postPackageExists.EndDate;
                await _uow.PostPackages.UpdateAsync(postPackageExists);
                var orderCode = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                PaymentRequest paymentRequest = new PaymentRequest
                {
                    Amount = postPackageExists.TotalAmout ?? 0,
                    Currency = postPackageExists.Currency ?? EnumCurrency.VND,
                    TypeTransaction = EnumTypeTransaction.PACKAGE_PURCHASE,
                    PaymentMethod = EnumPaymentMethod.PayOS,
                    AccountId = postPackageExists.AccountId ?? Guid.Empty
                };
                var payment = _mapper.Map<DAL.Entites.Transaction>(paymentRequest);
                payment.Status = EnumStatusPayment.PENDING;
                payment.PostPackageId = postPackageExists.Id;
                payment.TransactionCode = orderCode.ToString();
                await _uow.Transactions.CreateAsync(payment);
                await _uow.SaveChangesWithTransactionAsync();

                ItemData item = new ItemData(packageExists.PackageName ?? "Unknown Package", 1, (int)(postPackageExists.TotalAmout ?? 0));
                List<ItemData> items = new List<ItemData> { item };

                PaymentData paymentData = new PaymentData(
                    orderCode,
                    (int)(postPackageExists.TotalAmout ?? 0),
                    "Đơn hàng phục vụ học tập",
                    items,
                    $"https://modernestate.vercel.app/create-post-success",
                    $"https://modernestate.vercel.app/create-post-failure"
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

        public async Task<bool> VerifyPaymentAsync(WebhookType type)
        {
            try
            {
                var data = _payOS.verifyPaymentWebhookData(type);
                var transaction = await _uow.Transactions.FinByTransactionCode(data.orderCode.ToString());
                if (transaction == null)
                {
                    _logger.LogWarning(
                                   "No transaction found for order code {OrderCode}, skipping webhook processing.",
                                   data.orderCode
                               );
                    return true;
                }

                if (data.code == "00")
                {
                    _logger.LogInformation("Payment verified successfully: {Code}", data.code);
                    transaction.Status = EnumStatusPayment.SUCCESS;
                    if (transaction.PostPackageId == null)
                        throw new AppException(ErrorCode.NOT_FOUND, "PostPackageId không được null.");
                    var postPackage = await _uow.PostPackages.FindById(transaction.PostPackageId.Value);
                    if (postPackage == null)
                        throw new AppException(ErrorCode.NOT_FOUND, "Gói đăng bài không tồn tại.");
                    postPackage.Status = EnumStatus.ACTIVE;
                    await _uow.PostPackages.UpdateAsync(postPackage);
                    if (postPackage.PostId == null)
                        throw new AppException(ErrorCode.NOT_FOUND, "PostId không được null.");
                    var post = await _uow.Posts.FindById(postPackage.PostId.Value);
                    if (post == null)
                        throw new AppException(ErrorCode.NOT_FOUND, "Bài đăng không tồn tại.");
                    post.SourceStatus = EnumSourceStatus.WAIT_APPROVE;
                    post.PriorityStatus = postPackage.Package != null ? postPackage.Package.PriorityStatus : post.PriorityStatus;
                    await _uow.Posts.UpdateAsync(post);
                    _logger.LogInformation("Post package and post updated successfully for transaction {TransactionId}", transaction.Id);
                    var property = await _uow.Properties.FindById(post.PropertyId);
                    if (property == null)
                        throw new AppException(ErrorCode.NOT_FOUND, "Bất động sản không tồn tại.");
                    property.StatusSource = EnumSourceStatus.WAIT_APPROVE;
                    property.PriorityStatus = post.PriorityStatus;
                    await _uow.Properties.UpdateAsync(property);
                    _logger.LogInformation("Property updated successfully for transaction {TransactionId}", transaction.Id);
                }
                else
                {
                    _logger.LogWarning("Payment failed or canceled: {Code}", data.code);

                    transaction.Status = EnumStatusPayment.FAILED;
                }

                await _uow.Transactions.UpdateAsync(transaction);
                await _uow.SaveChangesWithTransactionAsync();

                return data.code == "00";
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