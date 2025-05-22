

using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.NewServices
{
    public class NewService : INewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<bool> CreateAsync(NewsRequest newsRequest)
        {
            var account = await _unitOfWork.Accounts.FindById(newsRequest.AccountId);

            var news = _mapper.Map<New>(newsRequest);
            news.Account = account;

            _unitOfWork.News.Create(news);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
    }
}