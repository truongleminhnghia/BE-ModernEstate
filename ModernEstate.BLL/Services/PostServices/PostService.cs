using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.BLL.Services.ContactServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.Common.srcs;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<PostService> _logger;
        private readonly IContactService _contactService;
        private readonly Utils _utils;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<PostService> logger,
                           IContactService contactService, Utils utils)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _contactService = contactService;
            _utils = utils;
        }

        public async Task<bool> CreatePost(PostRequest request)
        {
            try
            {
                Guid conactId = await _contactService.GetOrCreateAsync(request.Contact);
                if (conactId == Guid.Empty) throw new AppException(ErrorCode.NOT_NULL);
                var post = _mapper.Map<Post>(request);
                post.ContactId = conactId;
                post.Code = await _utils.GenerateUniqueBrokerCodeAsync("PRO_");
                await _unitOfWork.Posts.CreateAsync(post);
                await _unitOfWork.PostPackages.CreateAsync(_mapper.Map<PostPackage>(request.PostPackage));
                await _unitOfWork.SaveChangesWithTransactionAsync();
                _logger.LogInformation("Post created successfully with ID: {Id}", post.Id);
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

        public async Task<bool> DeletePost(Guid id)
        {
            try
            {
                var postExisting = await _unitOfWork.Posts.GetByIdAsync(id);
                if (postExisting == null) throw new AppException(ErrorCode.NOT_FOUND);
                await _unitOfWork.Posts.DeleteAsync(postExisting);
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

        public async Task<PostResponse?> GetByCode(string code)
        {
            try
            {
                var postExisting = await _unitOfWork.Posts.FindByCode(code);
                if (postExisting == null) throw new AppException(ErrorCode.NOT_FOUND);
                return _mapper.Map<PostResponse>(postExisting);
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

        public async Task<PostResponse?> GetById(Guid id)
        {
            try
            {
                var postExisting = await _unitOfWork.Posts.FindById(id);
                if (postExisting == null) throw new AppException(ErrorCode.NOT_FOUND);
                return _mapper.Map<PostResponse>(postExisting);
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

        public async Task<PageResult<PostResponse>> GetPosts(string? title, EnumStatePost? state, EnumSourceStatus? srcStatus, int pageCurrent, int pageSize)
        {
            try
            {
                var result = await _unitOfWork.Posts.FindWithParams(title, state, srcStatus);
                if (result == null) throw new AppException(ErrorCode.LIST_EMPTY);
                var pagedResult = result.Skip((pageCurrent - 1) * pageSize).Take(pageSize).ToList();
                var total = result.Count();
                var data = _mapper.Map<List<PostResponse>>(pagedResult);
                if (data == null || !data.Any()) throw new AppException(ErrorCode.LIST_EMPTY);
                var pageResult = new PageResult<PostResponse>(data, pageSize, pageCurrent, total);
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

        public Task<bool> UpdatePost(Guid id, UpdatePostRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
