

using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        public NewService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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
            news.CreatedAt = DateTime.Now;
            news.UpdatedAt = DateTime.Now;
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

        public async Task<bool> UpdateTitle(string name, Guid id)
        {
            var news = await _unitOfWork.News.GetByIdAsync(id);
            news.Title = name;
            news.UpdatedAt = DateTime.Now;
            await _unitOfWork.News.UpdateAsync(news);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}