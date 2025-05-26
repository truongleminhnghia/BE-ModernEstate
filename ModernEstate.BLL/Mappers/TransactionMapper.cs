using AutoMapper;
using ModernEstate.Common.Models.Responses;
using ModernEstate.DAL.Entites;

namespace ModernEstate.BLL.Mappers
{
    public class TransactionMapper : Profile
    {
        public TransactionMapper()
        {
            CreateMap<Transaction, CashPaymentResponse>()
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(
                    dest => dest.Currency,
                    opt => opt.MapFrom(src => src.Currency.ToString())
                );

            CreateMap<Transaction, VNPayPaymentResponse>()
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(
                    dest => dest.Currency,
                    opt => opt.MapFrom(src => src.Currency.ToString())
                )
                .ForMember(dest => dest.PaymentUrl, opt => opt.Ignore());
        }
    }
}
