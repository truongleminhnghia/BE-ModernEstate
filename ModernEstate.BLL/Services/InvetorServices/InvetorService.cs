
using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.Common.srcs;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.InvetorServices
{
    public class InvetorService : IInvetorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<InvetorService> _logger;
        private readonly Utils _utils;

        public InvetorService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<InvetorService> logger, Utils utils)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _utils = utils;
        }

        public async Task<bool> CreateInventor(InvetorRequest req)
        {
            try
            {
                var existing = await _unitOfWork.Invetors.FindByName(req.Name);
                if (existing != null)
                    throw new AppException(ErrorCode.HAS_EXISTED);
                var invetor = _mapper.Map<Invetor>(req);
                invetor.Code = await _utils.GenerateUniqueBrokerCodeAsync("INV_");
                invetor.CreatedAt = DateTime.UtcNow;
                invetor.UpdatedAt = DateTime.UtcNow;
                await _unitOfWork.Invetors.CreateAsync(invetor);
                await _unitOfWork.SaveChangesWithTransactionAsync();
                return true;
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

        public async Task<InvetorResponse> GetById(Guid id)
        {
            try
            {
                var existing = await _unitOfWork.Invetors.FindById(id);
                if (existing == null) throw new AppException(ErrorCode.NOT_FOUND);
                return _mapper.Map<InvetorResponse>(existing);
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

        public async Task<PageResult<InvetorResponse>> GetWithParams(string? name, string? companyName, EnumInvetorType? InvetorType, string email, int pageCurrent, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.Invetors.FindInvetors(name, companyName, InvetorType, email);
                if (result == null) throw new AppException(ErrorCode.LIST_EMPTY);
                var pagedResult = result.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                var total = result.Count();
                var data = _mapper.Map<List<InvetorResponse>>(pagedResult);
                if (data == null || !data.Any()) throw new AppException(ErrorCode.LIST_EMPTY);
                var pageResult = new PageResult<InvetorResponse>(data, pageSize, pageCurrent, total);
                return pageResult;
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

        public Task<bool> UpdateAccount(UpdateAccountRequest req, Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAccountStatus(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}