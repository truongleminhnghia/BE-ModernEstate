
using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.Roles
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RoleService> _logger;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, ILogger<RoleService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<bool> Create(EnumRoleName name)
        {
            try
            {
                var role = await _unitOfWork.Roles.GetByName(name);
                if (role != null)
                {
                    throw new AppException(ErrorCode.ROLE_HAS_EXISTED);
                }
                else
                {
                    role = new Role
                    {
                        Id = Guid.NewGuid(),
                        RoleName = name
                    };
                    await _unitOfWork.Roles.CreateAsync(role);
                    await _unitOfWork.SaveChangesWithTransactionAsync();
                    return true;
                }
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

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var role = await _unitOfWork.Roles.GetByIdAsync(id);
                if (role == null)
                {
                    throw new AppException(ErrorCode.NOT_FOUND);
                }
                await _unitOfWork.Roles.DeleteAsync(role);
                await _unitOfWork.SaveChangesWithTransactionAsync();
                return true;
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "Exception occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public Task<bool> Edit(Guid id, string name)
        {
            throw new NotImplementedException();
        }

        public async Task<RoleResponse> GetByName(EnumRoleName name)
        {
            try
            {
                var role = await _unitOfWork.Roles.GetByName(name);
                if (role == null)
                {
                    throw new AppException(ErrorCode.NOT_FOUND);
                }
                return _mapper.Map<RoleResponse>(role);
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "Exception occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<RoleResponse> GetRole(Guid id)
        {
            try
            {
                var role = await _unitOfWork.Roles.GetByIdAsync(id);
                if (role == null)
                {
                    throw new AppException(ErrorCode.NOT_FOUND);
                }
                return _mapper.Map<RoleResponse>(role);
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "Exception occurred: {Message}", ex.Message);
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