﻿using AutoMapper;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using BusinessLayer.Services;
using DataAccessLayer.Entities;
using DataAccessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            CreateMap<Appointment, AppointmentResponseModel>();
            CreateMap<AppointmentRequestModel, Appointment>()
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => AppointmentStatus.CREATED.ToString()));
            CreateMap<AppointmentRequestModelForUpdate, Appointment>();
            CreateMap<PaymentResponseModel, Payment>()
            .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => Helper.ExtractAmountFromOrderDescription(src.OrderDescription)))
            .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.Success ? PaymentStatus.SUCCESS.ToString() : PaymentStatus.FAILED.ToString()))
            .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => Helper.ExtractAppointmentIdFromOrderDescription(src.OrderDescription)));
        }
    }
}
