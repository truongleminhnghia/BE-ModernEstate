

using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.Common.srcs;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.PostPackageServices
{
    public class PostPackageService : IPostPackageService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly Utils _utils;
        private readonly ILogger<PostPackageService> _logger;

        public PostPackageService(IUnitOfWork uow, IMapper mapper, ILogger<PostPackageService> logger, Utils utils)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
            _utils = utils;
        }
        public async Task<PostPackage> CreateAsync(PostPackageReuqest request, Guid id)
        {
            try
            {
                if (request.AccountId == null)
                    throw new AppException(ErrorCode.NOT_NULL, "AccountId không được null.");
                var accountExists = await _uow.Accounts.GetByIdAsync(request.AccountId.Value);

                if (accountExists == null)
                {
                    _logger.LogWarning("Account with ID {AccountId} does not exist.", request.AccountId);
                    throw new AppException(ErrorCode.NOT_FOUND, "Người dùng không tồn tại.");
                }
                var postPackage = _mapper.Map<PostPackage>(request);
                postPackage.PostId = id;
                postPackage.Status = EnumStatus.INACTIVE;

                postPackage.AccountId = request.AccountId;
                await _uow.PostPackages.CreateAsync(postPackage);
                return postPackage;
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

        public Task<PostPackageResponse?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
