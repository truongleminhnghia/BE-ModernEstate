

using AutoMapper;
using Microsoft.Extensions.Logging;
using ModernEstate.BLL.Services.AuthenticateServices;
using ModernEstate.Common.Enums;
using ModernEstate.Common.Exceptions;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Services.BrokerServices
{
    public class BrokerService : IBrokerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthenticateService> _logger;

        public BrokerService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AuthenticateService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<BrokerRegisterResponse> RegisterBroker(Guid accountId)
        {
            var account = await _unitOfWork.Accounts.GetByIdAsync(accountId);
            if (account == null)
                return BrokerRegisterResponse.Fail("Account not found.");
            // Update role to broker
            var brokerRole = await _unitOfWork.Roles.GetByName(EnumRoleName.ROLE_BROKER);
            // Check duplicate
            if (account.RoleId == brokerRole.Id)
            {
                return BrokerRegisterResponse.Fail("Account is already registered as a broker.");
            }
            if (account.RoleId != brokerRole.Id)
            {
                account.RoleId = brokerRole.Id;
                _unitOfWork.Accounts.Update(account);
            }
            
            var broker = new Broker
            {
                Code = await GenerateUniqueBrokerCodeAsync(),
                AccountId = accountId
            };
            await _unitOfWork.Brokers.CreateAsync(broker);
            await _unitOfWork.SaveChangesAsync();

            return BrokerRegisterResponse.Ok("Broker registered successfully."); ;
        }

        private async Task<string> GenerateUniqueBrokerCodeAsync()
        {
            string code;
            bool exists;

            do
            {
                code = $"BRK{Random.Shared.Next(1000000, 9999999)}";
                exists = await _unitOfWork.Brokers
                    .AnyAsync(b => b.Code == code);
            }
            while (exists);

            return code;
        }

    }
}