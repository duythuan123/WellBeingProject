using AutoMapper;
using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using System.Text.RegularExpressions;
using UberSystem.Domain.Enums;

namespace BusinessLayer.Services
{
    public class PsychiatristService : IPsychiatristService
    {
        private readonly IPsychiatristRepository _pRepo;
        private readonly IUserRepository _uRepo;
        private readonly IMapper _mapper;
        public PsychiatristService(IPsychiatristRepository repo, IUserRepository uRepo, IMapper mapper)
        {
            _pRepo = repo;
            _uRepo = uRepo;
            _mapper = mapper;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task<BaseResponseModel<IEnumerable<PsychiatristResponseModel>>> GetAllPsychiatristsAsync()
        {
            var psychiatrists = await _pRepo.GetAllPsychiatristsAsync();

            var response = psychiatrists.Select(p => new PsychiatristResponseModel
            {
                Id = p.Id,
                UserId = p.UserId,
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
                ConsultationFee = p.ConsultationFee,

                TimeSlots = p.TimeSlots.Select(t => new TimeSlotResponseModel
                {
                    TimeSlotId = t.TimeSlotId,
                    StartTime = t.StartTime,
                    EndTime = t.EndTime,
                    SlotDate = t.SlotDate,
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
                    Id = existedPsychiatrist.Id,
                    UserId = existedPsychiatrist.UserId,
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
                    ConsultationFee = existedPsychiatrist.ConsultationFee,

                    TimeSlots = existedPsychiatrist.TimeSlots.Select(t => new TimeSlotResponseModel
                    {
                        TimeSlotId = t.TimeSlotId,
                        StartTime = t.StartTime,
                        EndTime = t.EndTime,
                        SlotDate = t.SlotDate,
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
            existedPsychiatrist.User.DateOfBirth = request.DateOfBirth != default 
                ? request.DateOfBirth
                : existedPsychiatrist.User.DateOfBirth;
            existedPsychiatrist.User.Phonenumber = request.Phonenumber ?? existedPsychiatrist.User.Phonenumber;
            existedPsychiatrist.User.Address = request.Address ?? existedPsychiatrist.User.Address;
            existedPsychiatrist.User.Gender = request.Gender ?? existedPsychiatrist.User.Gender;
            existedPsychiatrist.User.UserImage = request.UserImage ?? existedPsychiatrist.User.UserImage;

            existedPsychiatrist.Bio = request.Bio ?? existedPsychiatrist.Bio;
            existedPsychiatrist.Specialization = request.Specialization ?? existedPsychiatrist.Specialization;
            existedPsychiatrist.Experience = request.Experience ?? existedPsychiatrist.Experience;
            existedPsychiatrist.Location = request.Location ?? existedPsychiatrist.Location;
            existedPsychiatrist.ConsultationFee = request.ConsultationFee ?? existedPsychiatrist.ConsultationFee;

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
                    Location = existedPsychiatrist.Location,
                    ConsultationFee = existedPsychiatrist.ConsultationFee
                }
            };
        }

        public async Task<BaseResponseModel<PsychiatristResponseModel>> AddPsychiatristAsync(PsychiatristRequestModel request)
        {
            var existedUser = await _uRepo.GetUserByEmailAsync(request.User.Email);
            if (existedUser != null)
            {
                return new BaseResponseModel<PsychiatristResponseModel>
                {
                    Code = 400,
                    Message = "User already exists",
                    Data = null
                };
            }

            var newUser = _mapper.Map<User>(request.User);
            newUser.Role = UserRole.PSYCHIATRIST.ToString();

            try
            {
                await _uRepo.AddUserAsync(newUser);

                var createdUser = await _uRepo.GetUserByEmailAsync(newUser.Email);

                var newPsychiatrist = new Psychiatrist
                {
                    UserId = createdUser.Id,
                    Bio = request.Bio,
                    Specialization = request.Specialization,
                    Experience = request.Experience,
                    Location = request.Location,
                    ConsultationFee = request.ConsultationFee
                };

                await _pRepo.AddAsync(newPsychiatrist);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<PsychiatristResponseModel>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            var createdPsychiatrist = await _pRepo.GetPsychiatristByEmail(newUser.Email);

            return new BaseResponseModel<PsychiatristResponseModel>
            {
                Code = 200,
                Message = "Psychiatrist Created Success",
                Data = new PsychiatristResponseModel
                {
                    Id = createdPsychiatrist.Id,
                    UserId = createdPsychiatrist.UserId,
                    Fullname = createdPsychiatrist.User.Fullname,
                    Email = createdPsychiatrist.User.Email,
                    DateOfBirth = createdPsychiatrist.User.DateOfBirth,
                    Phonenumber = createdPsychiatrist.User.Phonenumber,
                    Address = createdPsychiatrist.User.Address,
                    Gender = createdPsychiatrist.User.Gender,
                    UserImage = createdPsychiatrist.User.UserImage,
                    Bio = createdPsychiatrist.Bio,
                    Specialization = createdPsychiatrist.Specialization,
                    Experience = createdPsychiatrist.Experience,
                    Location = createdPsychiatrist.Location,
                    ConsultationFee = createdPsychiatrist.ConsultationFee
                }
            };
        }
    }
}
