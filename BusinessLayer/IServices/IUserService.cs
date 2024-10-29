using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;

namespace BusinessLayer.IServices
{
    public interface IUserService
    {
        Task<BaseResponseModel<IEnumerable<UserResponseModel>>> GetAllUsersAsync();
        Task<BaseResponseModel<LoginResponseModel>> LoginAsync(LoginRequestModel request);
        Task<BaseResponseModel<UserResponseModel>> AddAsync(UserRequestModel request);
        Task<BaseResponseModel<UserResponseModel>> UpdateAsync(UserRequestModelForUpdate request, int id);
        Task<BaseResponseModel> DeleteAsync(int id);
        Task<BaseResponseModel<UserResponseModel>> GetDetailAsync(int id);
        Task<BaseResponseModel> ForgotPasswordAsync(string email);
        Task<BaseResponseModel> ResetPasswordAsync(string token, string newPassword);
        Task<BaseResponseModel<DashboardResponseModel>> GetDashBoard();
    }
}
