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

namespace ModernEstate.BLL.Services.PackageServices
{
    public class PackageService : IPackageService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly Utils _utils;
        private readonly ILogger<PackageService> _logger;

        public PackageService(IUnitOfWork uow, IMapper mapper, ILogger<PackageService> logger, Utils utils)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
            _utils = utils;
        }

        public async Task<IEnumerable<PackageResponse>> GetAllAsync()
        {
            try
            {
                var entities = await _uow.Packages.GetAllAsync();
                return _mapper.Map<IEnumerable<PackageResponse>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all packages");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<PackageResponse> CreateAsync(PackageRequest request)
        {
            try
            {
                var entity = _mapper.Map<Package>(request);
                // entity.Id = Guid.NewGuid();
                entity.PackageCode = await _utils.GenerateUniqueBrokerCodeAsync("EMP");
                await _uow.Packages.CreateAsync(entity);
                await _uow.SaveChangesWithTransactionAsync();

                return _mapper.Map<PackageResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create package");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<PackageResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _uow.Packages.GetByIdAsync(id);
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

        public async Task<bool> UpdateAsync(Guid id, PackageRequest request)
        {
            try
            {
                var entity = await _uow.Packages.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _mapper.Map(request, entity);
                _uow.Packages.Update(entity);
                await _uow.SaveChangesWithTransactionAsync();

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
                var entity = await _uow.Packages.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _uow.Packages.Delete(entity);
                await _uow.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete package id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<PageResult<PackageResponse>> GetWithParamsAsync(
            EnumTypePackage? typePackage,
            int pageCurrent,
            int pageSize
        )
        {
            try
            {
                var all = await _uow.Packages.FindPackagesAsync(typePackage);
                if (!all.Any())
                    throw new AppException(ErrorCode.LIST_EMPTY);

                var paged = all.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();

                var dtos = _mapper.Map<List<PackageResponse>>(paged);
                return new PageResult<PackageResponse>(dtos, pageSize, pageCurrent, all.Count());
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get packages with params");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}
