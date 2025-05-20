
using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.BLL.Services.AuthenticateServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.OwnerPropertyServices
{
    public class OwnerPropertyService : IOwnerPropertyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticateService> _logger;

        public OwnerPropertyService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AuthenticateService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<PropertyOwnerRegisterResponse> RegisterPropertyOwner(Guid accountId)
        {
            var account = await _unitOfWork.Accounts.GetByIdAsync(accountId);
            if (account == null)
                return PropertyOwnerRegisterResponse.Fail("Invalid account");
            // Update role to PO
            var brokerRole = await _unitOfWork.Roles.GetByName(EnumRoleName.ROLE_PROPERTY_OWNER);
            // Check duplicate
            if (account.RoleId == brokerRole.Id)
            {
                return PropertyOwnerRegisterResponse.Fail("Account is already registered as a property owner.");
            }
            if (account.RoleId != brokerRole.Id)
            {
                account.RoleId = brokerRole.Id;
                _unitOfWork.Accounts.Update(account);
            }

            var ownerProperty = new OwnerProperty
            {
                Code = await GenerateUniqueBrokerCodeAsync(),
                AccountId = accountId
            };
            await _unitOfWork.OwnerProperties.CreateAsync(ownerProperty);
            await _unitOfWork.SaveChangesAsync();

            return PropertyOwnerRegisterResponse.Ok("Property owner registered successfully."); ;
        }

        private async Task<string> GenerateUniqueBrokerCodeAsync()
        {
            string code;
            bool exists;

            do
            {
                code = $"PRO{Random.Shared.Next(1000000, 9999999)}";
                exists = await _unitOfWork.Brokers
                    .AnyAsync(b => b.Code == code);
            }
            while (exists);

            return code;
        }
    }
}