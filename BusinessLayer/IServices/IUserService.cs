using BusinessLayer.Models.Request;
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
        Task<BaseResponseModel<TokenModel>> LoginAsync(LoginRequestModel request);
        Task<BaseResponseModel<User>> AddAsync(UserRequestModel request);
        Task<BaseResponseModel<User>> UpdateAsync(UserRequestModelForUpdate user, int id);
        Task<BaseResponseModel<User>> GetDetailAsync(int id);
        Task<BaseResponseModel> ForgotPasswordAsync(string email);
        Task<BaseResponseModel> ResetPasswordAsync(string token, string newPassword);
    }
}
