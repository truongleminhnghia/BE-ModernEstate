// ModernEstate.BLL.Services.AddressServices/AddressService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.AddressServices
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddressService> _logger;

        public AddressService(IUnitOfWork uow, IMapper mapper, ILogger<AddressService> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> CreateAddress(AddressRequest req)
        {
            try
            {
                var entity = _mapper.Map<Address>(req);
                entity.Id = Guid.NewGuid();
                await _uow.Addresses.CreateAsync(entity);
                await _uow.SaveChangesWithTransactionAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create address");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<Guid> GetOrCreateAsync(AddressRequest request)
        {
            try
            {
                var entity = _mapper.Map<Address>(request);
                return await _uow.Addresses.GetOrCreateAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get or create address");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<AddressResponse> CreateAsync(AddressRequest request)
        {
            try
            {
                var entity = _mapper.Map<Address>(request);
                entity.Id = Guid.NewGuid();
                await _uow.Addresses.CreateAsync(entity);
                await _uow.SaveChangesWithTransactionAsync();
                return _mapper.Map<AddressResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create address");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<AddressResponse?> GetById(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<AddressResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _uow.Addresses.GetByIdAsync(id);
                if (entity == null)
                    return null;
                return _mapper.Map<AddressResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get address by id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<IEnumerable<AddressResponse>> GetAllAsync()
        {
            try
            {
                var list = await _uow.Addresses.GetAllAsync();
                return _mapper.Map<IEnumerable<AddressResponse>>(list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all addresses");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, AddressRequest request)
        {
            try
            {
                var entity = await _uow.Addresses.GetByIdAsync(id);
                if (entity == null)
                    return false;
                _mapper.Map(request, entity);
                _uow.Addresses.Update(entity);
                await _uow.SaveChangesWithTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update address id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _uow.Addresses.GetByIdAsync(id);
                if (entity == null)
                    return false;
                _uow.Addresses.Delete(entity);
                await _uow.SaveChangesWithTransactionAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete address id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<PageResult<AddressResponse>> GetWithParamsAsync(
            string? city,
            string? district,
            string? ward,
            int pageCurrent,
            int pageSize
        )
        {
            try
            {
                var all = await _uow.Addresses.FindAddressesAsync(city, district, ward);
                if (!all.Any())
                    throw new AppException(ErrorCode.LIST_EMPTY);

                var paged = all.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();

                var dtos = _mapper.Map<List<AddressResponse>>(paged);
                return new PageResult<AddressResponse>(dtos, pageSize, pageCurrent, all.Count());
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get addresses with params");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}
