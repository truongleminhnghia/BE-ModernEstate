using AutoMapper;
using ModernEstate.Common.Models.Requests;
using ModernEstate.Common.Models.Responses;
using ModernEstate.Common.Models.AuthenticateResponse;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap<AccountRequest, Account>();
            CreateMap<RegisterRequest, Account>();
            CreateMap<Account, AccountResponse>();
            CreateMap<Account, AccountCurrent>();
        }
    }
}
