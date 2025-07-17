

using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Requests;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<ContactService> _logger;

        public ContactService(IUnitOfWork uow, IMapper mapper, ILogger<ContactService> logger)
        {
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Guid> GetOrCreateAsync(ContactRequest request)
        {
            try
            {
                var entity = _mapper.Map<Contact>(request);
                return await _uow.Contacts.GetOrCreateAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get or create address");
                throw new AppException(ErrorCode.INTERNAL_SERVER_ERROR);
            }
        }
    }
}