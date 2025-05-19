using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.PostPackageServices
{
    public class PostPackageService : IPostPackageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<PostPackage> _repository;

        public PostPackageService(
            IUnitOfWork unitOfWork,
            IGenericRepository<PostPackage> repository
        )
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<IEnumerable<PostPackageResponse>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(MapToResponse);
        }

        public async Task<PostPackageResponse?> GetByIdAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            return entity == null ? null : MapToResponse(entity);
        }

        public async Task<PostPackageResponse> CreateAsync(PostPackageRequest request)
        {
            var entity = new PostPackage
            {
                Id = Guid.NewGuid(),
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Status = request.Status,
                AccountId = request.AccountId,
                PackageId = request.PackageId,
                PostId = request.PostId,
            };

            _repository.Create(entity);
            await _unitOfWork.SaveChangesWithTransactionAsync();

            return MapToResponse(entity);
        }

        public async Task<bool> UpdateAsync(Guid id, PostPackageRequest request)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            entity.StartDate = request.StartDate;
            entity.EndDate = request.EndDate;
            entity.Status = request.Status;
            entity.AccountId = request.AccountId;
            entity.PackageId = request.PackageId;
            entity.PostId = request.PostId;

            _repository.Update(entity);
            await _unitOfWork.SaveChangesWithTransactionAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return false;

            _repository.Delete(entity);
            await _unitOfWork.SaveChangesWithTransactionAsync();

            return true;
        }

        private PostPackageResponse MapToResponse(PostPackage entity) =>
            new PostPackageResponse
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Status = entity.Status,
                AccountId = entity.AccountId,
                PackageId = entity.PackageId,
                PostId = entity.PostId,
            };
    }
}
