using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using BusinessLayer.Utilities;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class PsychiatristService : IPsychiatristService
    {
        private readonly IConfiguration _configuration;
        private readonly IPsychiatristRepository _pRepo;
        private readonly IUserRepository _uRepo;

        public PsychiatristService(IConfiguration configuration, IPsychiatristRepository repo)
        {
            _configuration = configuration;
            _pRepo = repo;
        }

        public async Task<BaseResponseModel<PsychiatristResponseModel>> GetPsychiatristDetailAsync(int userId)
        {
            // Kiểm tra xem Psychiatrist có tồn tại không
            var existedPsychiatrist = await _pRepo.GetPsychiatristByUserId(userId);
            if (existedPsychiatrist == null)
            {
                return new BaseResponseModel<PsychiatristResponseModel>
                {
                    Code = 500,
                    Message = "Psychiatrist not exists",
                    Data = null
                };
            }

            // Trả về thông tin chi tiết nếu Psychiatrist tồn tại
            return new BaseResponseModel<PsychiatristResponseModel>
            {
                Code = 200,
                Message = "Get Psychiatrist Detail Success",
                Data = new PsychiatristResponseModel()
                {
                    Fullname = existedPsychiatrist.User.Fullname,
                    Email = existedPsychiatrist.User.Email,
                    DateOfBirth = existedPsychiatrist.User.DateOfBirth,
                    Phonenumber = existedPsychiatrist.User.Phonenumber,
                    Address = existedPsychiatrist.User.Address,
                    Gender = existedPsychiatrist.User.Gender,

                    Bio = existedPsychiatrist.Bio,
                    Specialization = existedPsychiatrist.Specialization,
                    Experience = existedPsychiatrist.Experience,
                    Location = existedPsychiatrist.Location
                },
            };
        }


        public async Task<BaseResponseModel<PsychiatristResponseModel>> UpdatePsychiatristAsync(PsychiatristRequestModelForUpdate request, int userId)
        {
            var existedPsychiatrist = await _pRepo.GetPsychiatristByUserId(userId);
            if (existedPsychiatrist == null)
            {
                return new BaseResponseModel<PsychiatristResponseModel>
                {
                    Code = 500,
                    Message = "Psychiatrist not exists",
                    Data = null
                };
            }

            // Cập nhật thông tin từ UpdateModel
            existedPsychiatrist.User.Fullname = request.Fullname ?? existedPsychiatrist.User.Fullname;
            existedPsychiatrist.User.Email = request.Email ?? existedPsychiatrist.User.Email;
            existedPsychiatrist.User.Phonenumber = request.Phonenumber ?? existedPsychiatrist.User.Phonenumber;
            existedPsychiatrist.User.Address = request.Address ?? existedPsychiatrist.User.Address;
            existedPsychiatrist.User.Gender = request.Gender ?? existedPsychiatrist.User.Gender;

            existedPsychiatrist.Bio = request.Bio ?? existedPsychiatrist.Bio;
            existedPsychiatrist.Specialization = request.Specialization ?? existedPsychiatrist.Specialization;
            existedPsychiatrist.Experience = request.Experience ?? existedPsychiatrist.Experience;
            existedPsychiatrist.Location = request.Location ?? existedPsychiatrist.Location;

            await _pRepo.UpdatePsychiatristAsync(existedPsychiatrist, userId);
            // Trả về response
            
            return new BaseResponseModel<PsychiatristResponseModel>
            {
                Code = 200,
                Message = "Update Psychiatrist Success",
                Data = new PsychiatristResponseModel
                {
                    Fullname = existedPsychiatrist.User.Fullname,
                    Email = existedPsychiatrist.User.Email,
                    DateOfBirth = existedPsychiatrist.User.DateOfBirth,
                    Phonenumber = existedPsychiatrist.User.Phonenumber,
                    Address = existedPsychiatrist.User.Address,
                    Gender = existedPsychiatrist.User.Gender,
                    Bio = existedPsychiatrist.Bio,
                    Specialization = existedPsychiatrist.Specialization,
                    Experience = existedPsychiatrist.Experience,
                    Location = existedPsychiatrist.Location
                }
            };
        }
    }
}
