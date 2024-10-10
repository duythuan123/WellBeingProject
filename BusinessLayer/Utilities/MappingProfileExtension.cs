using AutoMapper;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using BusinessLayer.Services;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Utilities
{
    public class MappingProfileExtension : Profile
    {
        public MappingProfileExtension()
        {
            CreateMap<User, UserResponseModel>();
            CreateMap<UserRequestModel, User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => Helper.HashPassword(src.Password)));
            CreateMap<UserRequestModelForUpdate, User>();
        }
    }
}
