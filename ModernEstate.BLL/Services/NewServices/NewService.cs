using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModernEstate.BLL.Services.AccountServices;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.ApiResponse;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
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
            var newsExist = await _unitOfWork.News.FindByTitle(request.Title);
            if (newsExist != null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "A news article with the same title already exists."
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

            _mapper.Map(request, news);

            _unitOfWork.News.Update(news);
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

    }
}