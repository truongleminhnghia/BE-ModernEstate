
using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.AddressServices
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddressService> _logger;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddressService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<AddressResponse>> GetAllAsync()
        {
            try
            {
                var entities = await _unitOfWork.Addresses.GetAllAsync();
                return _mapper.Map<IEnumerable<AddressResponse>>(entities);
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

        public async Task<Guid> CreateAddress(AddressRequest req)
        {
            try
            {
                var addressEntity = _mapper.Map<Address>(req);
                Guid addressId = await _unitOfWork.Addresses.GetOrCreateAsync(addressEntity);
                return addressId;
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

        public async Task<AddressResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Addresses.GetByIdAsync(id);
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

        public async Task<AddressResponse> CreateAsync(AddressRequest request)
        {
            try
            {
                var entity = _mapper.Map<Address>(request);
                entity.Id = Guid.NewGuid();

                await _unitOfWork.Addresses.CreateAsync(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return _mapper.Map<AddressResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create address");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, AddressRequest request)
        {
            try
            {
                var entity = await _unitOfWork.Addresses.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _mapper.Map(request, entity);

                _unitOfWork.Addresses.Update(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

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
                var entity = await _unitOfWork.Addresses.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _unitOfWork.Addresses.Delete(entity);
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

        public async Task<AddressResponse?> GetById(Guid id)
        {
            try
            {
                var address = await _unitOfWork.Addresses.GetByIdAsync(id);
                if (address == null) throw new AppException(ErrorCode.NOT_FOUND);
                return _mapper.Map<AddressResponse>(address);
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
