using AutoMapper;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging;
using ModernEstate.BLL.Services.AccountServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Pages;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.NewServices
{
    public class NewService : INewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<NewService> _logger;

        public NewService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<NewService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<NewsCreateResponse> CreateAsync(NewsRequest newsRequest)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("accountId");
            if (userIdClaim == null) return NewsCreateResponse.Fail("Invalid authorize.");
            var accountId = Guid.Parse(userIdClaim.Value);

            var category = await _unitOfWork.Categories.GetByIdAsync(newsRequest.CategoryId);
            if (category == null)
            {
                return NewsCreateResponse.Fail("Invalid category.");
            }
            var newsExist = await _unitOfWork.News.FindByTitle(newsRequest.Title);
            if (newsExist != null)
            {
                return NewsCreateResponse.Fail("News title already exist");
            }
            
            var news = _mapper.Map<New>(newsRequest);
            news.AccountId = accountId;
            var tagEntities = new List<Tag>();
            foreach (var tagName in newsRequest.TagNames.Distinct())
            {
                var existingTag = await _unitOfWork.Tags.FindByTitle(tagName);
                if (existingTag != null)
                {
                    tagEntities.Add(existingTag);
                }
                else
                {
                    var newTag = new Tag
                    {
                        TagName = tagName
                    };
                    _unitOfWork.Tags.Create(newTag);
                    tagEntities.Add(newTag);
                }
            }

            news.NewTags = tagEntities.Select(tag => new NewTag
            {
                NewId = news.Id,
                TagId = tag.Id
            }).ToList();

            _unitOfWork.News.Create(news);
            await _unitOfWork.SaveChangesAsync();
            return NewsCreateResponse.Ok("Create news successful");
        }

        public async Task<ApiResponse> UpdateAsync(Guid id, NewsRequest request)
        {
            var news = await _unitOfWork.News.GetByIdAsync(id);
            if (news == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "News not found"
                };
            }
            
            var category = await _unitOfWork.Categories.GetByIdAsync(request.CategoryId);
            if (category == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Invalid category."
                };
            }

            if (!string.Equals(news.Title, request.Title, StringComparison.OrdinalIgnoreCase))
            {
                var newsExist = await _unitOfWork.News.FindByTitle(request.Title);

                if (newsExist != null)
                {
                    return new ApiResponse
                    {
                        Success = false,
                        Message = "A news article with the same title already exists."
                    };
                }
            }

            _mapper.Map(request, news);
            _unitOfWork.News.Update(news);

            await _unitOfWork.NewTags.DeleteByNewIdAsync(news.Id);

            foreach (var tagName in request.TagNames.Distinct())
            {
                var tag = await _unitOfWork.Tags.FindByTitle(tagName);
                if (tag == null)
                {
                    tag = new Tag { TagName = tagName };
                    _unitOfWork.Tags.Create(tag);
                }

                var newTag = new NewTag
                {
                    NewId = news.Id,
                    TagId = tag.Id
                };
                _unitOfWork.NewTags.Create(newTag);
            }


            await _unitOfWork.SaveChangesAsync();

            return new ApiResponse
            {
                Success = true,
                Message = "News updated successfully"
            };

        }
        public async Task<NewsResponse> GetByIdAsync(Guid id)
        {
            try
            {
                var news = await _unitOfWork.News.GetByIdWithDetailsAsync(id);


                if (news == null)
                    throw new KeyNotFoundException("News not found.");

                return _mapper.Map<NewsResponse>(news);
            }
            catch (AppException ex)
            {
                _logger.LogWarning(ex, "AppException occurred: {Message}", ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
                throw new AppException(ErrorCode.NOT_FOUND);
            }
        }

        public async Task<EnumStatusNew> ToggleStatusAsync(Guid id)
        {
            var news = await _unitOfWork.News.GetByIdAsync(id);
            if (news == null)
                throw new Exception("News not found");

            news.StatusNew = news.StatusNew == EnumStatusNew.PUBLISHED
                ? EnumStatusNew.ARCHIVED
                : EnumStatusNew.PUBLISHED;

            _unitOfWork.News.Update(news);
            await _unitOfWork.SaveChangesAsync();

            return news.StatusNew;
        }

        public async Task<PageResult<NewsResponse>> GetWithParamsAsync(
    string? title,
    EnumStatusNew? status,
    EnumCategoryName? categoryName,
    string? tags,
    int pageCurrent,
    int pageSize,
    DateTime? startDate,
    DateTime? endDate,
    string sortBy = "title",
    bool sortDescending = false
)
        {
            try
            {
                var all = await _unitOfWork.News.FindNewsAsync(title, status, categoryName, tags,startDate,endDate, sortBy, sortDescending);
                if (!all.Any())
                    throw new AppException(ErrorCode.LIST_EMPTY);

                var paged = all
                    .Skip((pageCurrent - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                var dtoList = _mapper.Map<List<NewsResponse>>(paged);
                return new PageResult<NewsResponse>(dtoList, pageSize, pageCurrent, all.Count());
            }
            catch (AppException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get news with parameters");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }

    }
}