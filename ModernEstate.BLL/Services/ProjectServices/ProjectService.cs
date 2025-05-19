using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.ProjectServices
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProjectService> _logger;

        public ProjectService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<ProjectService> logger
        )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ProjectResponse>> GetAllAsync()
        {
            try
            {
                var entities = await _unitOfWork.Projects.GetAllAsync();
                return _mapper.Map<IEnumerable<ProjectResponse>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get all projects");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<ProjectResponse?> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Projects.GetByIdAsync(id);
                if (entity == null)
                    return null;
                return _mapper.Map<ProjectResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get project by id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<ProjectResponse> CreateAsync(ProjectRequest request)
        {
            try
            {
                var entity = _mapper.Map<Project>(request);
                entity.Id = Guid.NewGuid();

                await _unitOfWork.Projects.CreateAsync(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return _mapper.Map<ProjectResponse>(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create project");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, ProjectRequest request)
        {
            try
            {
                var entity = await _unitOfWork.Projects.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _mapper.Map(request, entity);

                _unitOfWork.Projects.Update(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to update project id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _unitOfWork.Projects.GetByIdAsync(id);
                if (entity == null)
                    return false;

                _unitOfWork.Projects.Delete(entity);
                await _unitOfWork.SaveChangesWithTransactionAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to delete project id {id}");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}
