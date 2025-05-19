using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Bases;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Post> _repository;

        public PostService(IUnitOfWork unitOfWork, IGenericRepository<Post> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<IEnumerable<PostResponse>> GetAllAsync()
        {
            var posts = await _repository.GetAllAsync();
            return posts.Select(MapToResponse);
        }

        public async Task<PostResponse?> GetByIdAsync(Guid id)
        {
            var post = await _repository.GetByIdAsync(id);
            return post == null ? null : MapToResponse(post);
        }

        public async Task<PostResponse> CreateAsync(PostRequest request)
        {
            var post = new Post
            {
                Id = Guid.NewGuid(),
                Code = request.Code,
                Title = request.Title,
                AppRovedBy = request.AppRovedBy,
                PostBy = request.PostBy,
                State = request.State,
                SourceStatus = request.SourceStatus,
                Description = request.Description,
                RejectionReason = request.RejectionReason,
                PropertyId = request.PropertyId,
                ContactId = request.ContactId,
                SupportId = request.SupportId,
            };

            await _repository.CreateAsync(post);
            await _unitOfWork.SaveChangesWithTransactionAsync();
            return MapToResponse(post);
        }

        public async Task<bool> UpdateAsync(Guid id, PostRequest request)
        {
            var post = await _repository.GetByIdAsync(id);
            if (post == null)
                return false;

            post.Code = request.Code;
            post.Title = request.Title;
            post.AppRovedBy = request.AppRovedBy;
            post.PostBy = request.PostBy;
            post.State = request.State;
            post.SourceStatus = request.SourceStatus;
            post.Description = request.Description;
            post.RejectionReason = request.RejectionReason;
            post.PropertyId = request.PropertyId;
            post.ContactId = request.ContactId;
            post.SupportId = request.SupportId;

            _repository.Update(post);
            await _unitOfWork.SaveChangesWithTransactionAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var post = await _repository.GetByIdAsync(id);
            if (post == null)
                return false;

            _repository.Delete(post);
            await _unitOfWork.SaveChangesWithTransactionAsync();
            return true;
        }

        private PostResponse MapToResponse(Post post) =>
            new PostResponse
            {
                Id = post.Id,
                Code = post.Code!,
                Title = post.Title!,
                AppRovedBy = post.AppRovedBy!,
                PostBy = post.PostBy!,
                State = post.State,
                SourceStatus = post.SourceStatus,
                Description = post.Description,
                RejectionReason = post.RejectionReason,
                PropertyId = post.PropertyId,
                ContactId = post.ContactId,
                SupportId = post.SupportId,
            };
    }
}
