using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.PackageServices
{
    public class PackageService : IPackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PackageService> _logger;

        public PackageService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PackageService> logger
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<PackageResponse>> GetAllAsync()
        {
            try
            {
                var entities = await _unitOfWork.Packages.GetAllAsync();
                return _mapper.Map<IEnumerable<PackageResponse>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all packages");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<PackageResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Packages.GetByIdAsync(id);
                if (entity == null)
                    return null;
                return _mapper.Map<PackageResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get package by id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<PackageResponse> CreateAsync(PackageRequest request)
        {
            try
            {
                var entity = _mapper.Map<Package>(request);
                entity.Id = Guid.NewGuid();

                await _unitOfWork.Packages.CreateAsync(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return _mapper.Map<PackageResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create package");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, PackageRequest request)
        {
            try
            {
                var entity = await _unitOfWork.Packages.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _mapper.Map(request, entity);

                _unitOfWork.Packages.Update(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update package id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Packages.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _unitOfWork.Packages.Delete(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete package id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}
