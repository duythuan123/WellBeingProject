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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class PsychiatristService : IPsychiatristService
    {
        private readonly IPsychiatristRepository _pRepo;

        public PsychiatristService(IPsychiatristRepository repo)
        {
            _pRepo = repo;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task<BaseResponseModel<IEnumerable<PsychiatristResponseModel>>> GetAllPsychiatristsAsync()
        {
            var psychiatrists = await _pRepo.GetAllPsychiatristsAsync();

            var response = psychiatrists.Select(p => new PsychiatristResponseModel
            {
                Fullname = p.User.Fullname,
                Email = p.User.Email,
                DateOfBirth = p.User.DateOfBirth,
                Phonenumber = p.User.Phonenumber,
                Address = p.User.Address,
                Gender = p.User.Gender,
                UserImage = p.User.UserImage,
                
                Bio = p.Bio,
                Specialization = p.Specialization,
                Experience = p.Experience,
                Location = p.Location,

                TimeSlots = p.TimeSlots.Select(t => new TimeSlotResponseModel
                {
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    DateOfWeek = t.DateOfWeek,
                }).ToList() // Ánh xạ các time slot
            }).ToList();

            return new BaseResponseModel<IEnumerable<PsychiatristResponseModel>>
            {
                Code = 200,
                Message = "Get all psychiatrists success!",
                Data = response
            };
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task<BaseResponseModel<PsychiatristResponseModel>> GetPsychiatristDetailAsync(int userId)
        {
            // Kiểm tra xem Psychiatrist có tồn tại không
            var existedPsychiatrist = await _pRepo.GetPsychiatristByUserId(userId);
            if (existedPsychiatrist == null)
            {
                return new BaseResponseModel<PsychiatristResponseModel>
                {
                    Code = 500,
                    Message = "Psychiatrist not exists!",
                    Data = null
                };
            }

            // Trả về thông tin chi tiết nếu Psychiatrist tồn tại
            return new BaseResponseModel<PsychiatristResponseModel>
            {
                Code = 200,
                Message = "Get psychiatrist detail success!",
                Data = new PsychiatristResponseModel()
                {
                    Fullname = existedPsychiatrist.User.Fullname,
                    Email = existedPsychiatrist.User.Email,
                    DateOfBirth = existedPsychiatrist.User.DateOfBirth,
                    Phonenumber = existedPsychiatrist.User.Phonenumber,
                    Address = existedPsychiatrist.User.Address,
                    Gender = existedPsychiatrist.User.Gender,
                    UserImage = existedPsychiatrist.User.UserImage,

                    Bio = existedPsychiatrist.Bio,
                    Specialization = existedPsychiatrist.Specialization,
                    Experience = existedPsychiatrist.Experience,
                    Location = existedPsychiatrist.Location,

                    TimeSlots = existedPsychiatrist.TimeSlots.Select(t => new TimeSlotResponseModel
                    {
                        StartTime = t.StartTime,
                        EndTime = t.EndTime,
                        DateOfWeek = t.DateOfWeek,
                    }).ToList()
                },
            };
        }

        ///-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task<BaseResponseModel<PsychiatristResponseModel>> UpdatePsychiatristAsync(PsychiatristRequestModelForUpdate request, int userId)
        {
            // Kiểm tra xem Psychiatrist có tồn tại không
            var existedPsychiatrist = await _pRepo.GetPsychiatristByUserId(userId);
            if (existedPsychiatrist == null)
            {
                return new BaseResponseModel<PsychiatristResponseModel>
                {
                    Code = 500,
                    Message = "Psychiatrist not exists!",
                    Data = null
                };
            }

            // Kiểm tra định dạng email
            if (string.IsNullOrEmpty(request.Email) ||
                !Regex.IsMatch(request.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                return new BaseResponseModel<PsychiatristResponseModel>
                {
                    Code = 400,
                    Message = "Invalid email format!",
                    Data = null
                };
            }

            // Kiểm tra định dạng số điện thoại
            if (string.IsNullOrEmpty(request.Phonenumber) ||
                !Regex.IsMatch(request.Phonenumber, @"^\d{10}$")) 
            {
                return new BaseResponseModel<PsychiatristResponseModel>
                {
                    Code = 400,
                    Message = "Invalid phone number format!",
                    Data = null
                };
            }

            // Kiểm tra trùng email 
            var existingEmailPsychiatrist = await _pRepo.GetPsychiatristByEmail(request.Email);
            if (existingEmailPsychiatrist != null && existingEmailPsychiatrist.UserId != userId)
            {
                return new BaseResponseModel<PsychiatristResponseModel>
                {
                    Code = 400,
                    Message = "Email already in use by another user!",
                    Data = null
                };
            }

            //Kiểm tra trùng số điện thoại
            var existingPhonePsychiatrist = await _pRepo.GetPsychiatristByPhoneNumber(request.Phonenumber);
            if (existingPhonePsychiatrist != null && existingPhonePsychiatrist.UserId != userId)
            {
                return new BaseResponseModel<PsychiatristResponseModel>
                {
                    Code = 400,
                    Message = "Phone number already in use by another user!",
                    Data = null
                };
            }

            existedPsychiatrist.User.Fullname = request.Fullname ?? existedPsychiatrist.User.Fullname;
            existedPsychiatrist.User.Email = request.Email ?? existedPsychiatrist.User.Email;
            existedPsychiatrist.User.Phonenumber = request.Phonenumber ?? existedPsychiatrist.User.Phonenumber;
            existedPsychiatrist.User.Address = request.Address ?? existedPsychiatrist.User.Address;
            existedPsychiatrist.User.Gender = request.Gender ?? existedPsychiatrist.User.Gender;
            existedPsychiatrist.User.UserImage = request.UserImage ?? existedPsychiatrist.User.UserImage;

            existedPsychiatrist.Bio = request.Bio ?? existedPsychiatrist.Bio;
            existedPsychiatrist.Specialization = request.Specialization ?? existedPsychiatrist.Specialization;
            existedPsychiatrist.Experience = request.Experience ?? existedPsychiatrist.Experience;
            existedPsychiatrist.Location = request.Location ?? existedPsychiatrist.Location;

            await _pRepo.UpdatePsychiatristAsync(existedPsychiatrist, userId);
            
            return new BaseResponseModel<PsychiatristResponseModel>
            {
                Code = 200,
                Message = "Update psychiatrist success!",
                Data = new PsychiatristResponseModel
                {
                    Fullname = existedPsychiatrist.User.Fullname,
                    Email = existedPsychiatrist.User.Email,
                    DateOfBirth = existedPsychiatrist.User.DateOfBirth,
                    Phonenumber = existedPsychiatrist.User.Phonenumber,
                    Address = existedPsychiatrist.User.Address,
                    Gender = existedPsychiatrist.User.Gender,
                    UserImage = existedPsychiatrist.User.UserImage,
                    Bio = existedPsychiatrist.Bio,
                    Specialization = existedPsychiatrist.Specialization,
                    Experience = existedPsychiatrist.Experience,
                    Location = existedPsychiatrist.Location
                }
            };
        }
    }
}
