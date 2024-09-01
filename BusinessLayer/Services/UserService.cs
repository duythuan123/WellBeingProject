using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using BusinessLayer.Utilities;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _repo;
        private readonly SendEmailSMTPServices _smtp;

        public UserService(IConfiguration configuration, IUserRepository repo, SendEmailSMTPServices smtp)
        {
            _configuration = configuration;
            _repo = repo;
            _smtp = smtp;
        }
        public async Task<BaseResponseModel<TokenModel>> LoginAsync(LoginRequestModel request)
        {
            var user = await _repo.GetUserByEmailAsync(request.Email);

            if (user != null && VerifyPassword(request.Password, user.Password))
            {
                string token = GenerateJwtToken(user);

                return new BaseResponseModel<TokenModel>()
                {
                    Code = 200,
                    Message = "Login Success",
                    Data = new TokenModel()
                    {
                        Token = token,
                    },
                };
            }
            return new BaseResponseModel<TokenModel>()
            {
                Code = 500,
                Message = "Username or Password incorrect",
                Data = new TokenModel()
                {
                    Token = null,
                },
            };
        }
        public async Task<BaseResponseModel<User>> AddAsync(UserRequestModel request)
        {
            //Check if user is existed (base on email)
            var existedUser = await _repo.GetUserByEmailAsync(request.Email);
            if (existedUser != null)
            {
                return new BaseResponseModel<User>
                {
                    Code = 500,
                    Message = "User already exists",
                    Data = null
                };
            }

            var newUser = new User()
            {
                Fullname = request.Fullname,
                Password = HashPassword(request.Password),
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                Phonenumber = request.Phonenumber,
                Address = request.Address,
                Gender = request.Gender
            };

            try
            {
                await _repo.AddUserAsync(newUser);
            }catch (Exception ex)
            {
                return new BaseResponseModel<User>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            return new BaseResponseModel<User>
            {
                Code = 200,
                Message = "User Created Success",
                Data = newUser
            };
        }
        public async Task<BaseResponseModel<User>> UpdateAsync(UserRequestModelForUpdate request, int id)
        {
            //Check if user is existed (base on id)
            var existedUser = await _repo.GetUserById(id);
            if (existedUser == null)
            {
                return new BaseResponseModel<User>
                {
                    Code = 500,
                    Message = "User not exists",
                    Data = null
                };
            }

            var editingUser = new User()
            {
                Fullname = request.Fullname,
                ///No edit for Password///
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                Phonenumber = request.Phonenumber,
                Address = request.Address,
                Gender = request.Gender
            };

            try
            {
                await _repo.UpdateUserAsync(editingUser, id);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<User>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            return new BaseResponseModel<User>
            {
                Code = 200,
                Message = "User Updated Success",
                Data = existedUser
            };
        }
        public async Task<BaseResponseModel<User>> GetDetailAsync(int id)
        {
            //Check if user is existed (base on id)
            var existedUser = await _repo.GetUserById(id);
            if (existedUser == null)
            {
                return new BaseResponseModel<User>
                {
                    Code = 500,
                    Message = "User not exists",
                    Data = null
                };
            }

            return new BaseResponseModel<User>
            {
                Code = 200,
                Message = "Get User Detail Success",
                Data = existedUser
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
                    Code = 500,
                    Message = "Invalid or expired token",
                };
            }

            user.Password = HashPassword(newPassword);

            await _repo.UpdatePasswordAsync(user, user.Id);

            return new BaseResponseModel
            {
                Code = 200,
                Message = "Password reset successfully",
            };
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Fullname)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        private string HashPassword(string password)
        {
            byte[] salt = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string hashedPassword = Convert.ToBase64String(hashBytes);

            return hashedPassword;
        }
        private bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            byte[] hash = new byte[20];
            Array.Copy(hashBytes, 16, hash, 0, 20);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] computedHash = pbkdf2.GetBytes(20);

            for (int i = 0; i < 20; i++)
            {
                if (hash[i] != computedHash[i])
                {
                    return false;
                }
            }
            return true;
        }
        
    }
}
