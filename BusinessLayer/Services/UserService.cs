using AutoMapper;
using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using BusinessLayer.Utilities;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _repo;
        private readonly SendEmailSMTPServices _smtp;
        private readonly IMapper _mapper;

        public UserService(IConfiguration configuration, IUserRepository repo, SendEmailSMTPServices smtp, IMapper mapper)
        {
            _configuration = configuration;
            _repo = repo;
            _smtp = smtp;
            _mapper = mapper;
        }
        public async Task<BaseResponseModel<LoginResponseModel>> LoginAsync(LoginRequestModel request)
        {
            var user = await _repo.GetUserByEmailAsync(request.Email);

            if (user != null && Helper.VerifyPassword(request.Password, user.Password))
            {
                string token = Helper.GenerateJwtToken(user, _configuration);

                return new BaseResponseModel<LoginResponseModel>()
                {
                    Code = 200,
                    Message = "Login Success",
                    Data = new LoginResponseModel()
                    {
                        Token = new TokenModel()
                        {
                            Token = token
                        },

                        User = _mapper.Map<UserResponseModel>(user)
                    },
                };
            }
            return new BaseResponseModel<LoginResponseModel>()
            {
                Code = 400,
                Message = "Username or Password incorrect",
                Data = new LoginResponseModel()
                {
                    Token = null,
                    User = null
                },
            };
        }
        public async Task<BaseResponseModel<UserResponseModel>> AddAsync(UserRequestModel request)
        {
            //Check if user is existed (base on email)
            var existedUser = await _repo.GetUserByEmailAsync(request.Email);
            if (existedUser != null)
            {
                return new BaseResponseModel<UserResponseModel>
                {
                    Code = 400,
                    Message = "User already exists",
                    Data = null
                };
            }

            var newUser = _mapper.Map<User>(request);

            try
            {
                await _repo.AddUserAsync(newUser);
            }catch (Exception ex)
            {
                return new BaseResponseModel<UserResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            return new BaseResponseModel<UserResponseModel>
            {
                Code = 200,
                Message = "User Created Success",
                Data = _mapper.Map<UserResponseModel>(newUser)
            };
        }
        public async Task<BaseResponseModel<UserResponseModel>> UpdateAsync(UserRequestModelForUpdate request, int id)
        {
            //Check if user is existed (base on id)
            var existedUser = await _repo.GetUserById(id);
            if (existedUser == null)
            {
                return new BaseResponseModel<UserResponseModel>
                {
                    Code = 404,
                    Message = "User not exists",
                    Data = null
                };
            }

            _mapper.Map(request, existedUser);

            try
            {
                await _repo.UpdateUserAsync(existedUser);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<UserResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            return new BaseResponseModel<UserResponseModel>
            {
                Code = 200,
                Message = "User Updated Success",
                Data = _mapper.Map<UserResponseModel>(existedUser)
            };
        }
        public async Task<BaseResponseModel<UserResponseModel>> GetDetailAsync(int id)
        {
            //Check if user is existed (base on id)
            var existedUser = await _repo.GetUserById(id);
            if (existedUser == null)
            {
                return new BaseResponseModel<UserResponseModel>
                {
                    Code = 404,
                    Message = "User not exists",
                    Data = null
                };
            }

            return new BaseResponseModel<UserResponseModel>
            {
                Code = 200,
                Message = "Get User Detail Success",
                Data = _mapper.Map<UserResponseModel>(existedUser)
            };
        }

        public async Task<BaseResponseModel> ForgotPasswordAsync(string email)
        {
            var user = await _repo.GetUserByEmailAsync(email);
            if (user == null)
            {
                return new BaseResponseModel
                {
                    Code = 404,
                    Message = "User not found",
                };
            }

            // Generate a reset token
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var tokenExpiration = DateTime.UtcNow.AddMinutes(10);
            await _repo.SavePasswordResetTokenAsync(user, token, tokenExpiration);

            // Create the reset URL(none for now)

            // Send the email
            string body = "Here is your token for reset password: " + token;

            try
            {
                await _smtp.SendEmailAsync(user.Email, "Password Reset", body);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Code = 500,
                    Message = ex.Message,
                };
            }

            return new BaseResponseModel
            {
                Code = 200,
                Message = "Password reset email sent", 
            };
        }

        public async Task<BaseResponseModel> ResetPasswordAsync(string token, string newPassword)
        {
            var user = await _repo.GetUserByPasswordResetTokenAsync(token);
            if (user == null)
            {
                return new BaseResponseModel
                {
                    Code = 400,
                    Message = "Invalid or expired token",
                };
            }

            user.Password = Helper.HashPassword(newPassword);

            await _repo.UpdatePasswordAsync(user, user.Id);

            return new BaseResponseModel
            {
                Code = 200,
                Message = "Password reset successfully",
            };
        }

        public async Task<BaseResponseModel<IEnumerable<UserResponseModel>>> GetAllUsersAsync()
        {
            var users = await _repo.GetAllUsersAsync();
            var userResponseModels = _mapper.Map<IEnumerable<UserResponseModel>>(users);

            if (users.Count() == 0)
            {
                return new BaseResponseModel<IEnumerable<UserResponseModel>>
                {
                    Code = 200,
                    Message = "No Users in the server",
                    Data = userResponseModels
                };
            }

            return new BaseResponseModel<IEnumerable<UserResponseModel>>
            {
                Code = 200,
                Message = "Users retrieved successfully",
                Data = userResponseModels
            };
        }

        public async Task<BaseResponseModel> DeleteAsync(int id)
        {
            var user = await _repo.GetUserById(id);
            if (user == null)
            {
                return new BaseResponseModel
                {
                    Code = 404,
                    Message = "User not found",
                };
            }

            try
            {
                await _repo.DeleteUserAsync(id);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel
                {
                    Code = 500,
                    Message = ex.Message,
                };
            }

            return new BaseResponseModel
            {
                Code = 200,
                Message = "User is Deleted Successfully",
            };
        }
    }
}
