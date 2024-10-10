﻿using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IServices
{
    public interface IUserService
    {
        Task<BaseResponseModel<IEnumerable<UserResponseModel>>> GetAllUsersAsync(string searchTerm);
        Task<BaseResponseModel<LoginResponseModel>> LoginAsync(LoginRequestModel request);
        Task<BaseResponseModel<UserResponseModel>> AddAsync(UserRequestModel request);
        Task<BaseResponseModel<UserResponseModel>> UpdateAsync(UserRequestModelForUpdate user, int id);
        Task<BaseResponseModel<UserResponseModel>> GetDetailAsync(int id);
        Task<BaseResponseModel> ForgotPasswordAsync(string email);
        Task<BaseResponseModel> ResetPasswordAsync(string token, string newPassword);
    }
}
