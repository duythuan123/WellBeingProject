using AutoMapper;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using UberSystem.Domain.Enums;

namespace BusinessLayer.Utilities
{
    public class MappingProfileExtension : Profile
    {
        public MappingProfileExtension()
        {
            CreateMap<User, UserResponseModel>();
            CreateMap<UserRequestModel, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Helper.HashPassword(src.Password)))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => UserRole.USER.ToString()));
            CreateMap<UserRequestModelForUpdate, User>();
            CreateMap<Appointment, AppointmentResponseModel>()
                .ForMember(dest => dest.ConsultationFee, opt => opt.MapFrom(src => src.Psychiatrist.ConsultationFee));

            CreateMap<AppointmentRequestModel, Appointment>()
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => AppointmentStatus.CREATED.ToString()));
            CreateMap<AppointmentRequestModelForUpdate, Appointment>()
                .ForMember(dest => dest.BookingDate, opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<PaymentResponseModel, Payment>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => Helper.ExtractAmountFromOrderDescription(src.OrderDescription)))
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.Success ? PaymentStatus.SUCCESS.ToString() : PaymentStatus.FAILED.ToString()))
            .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => Helper.ExtractAppointmentIdFromOrderDescription(src.OrderDescription)));
        }
    }
}
