using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using BusinessLayer.Utilities;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class PsychiatristService
    {
        private readonly IConfiguration _configuration;
        private readonly IPsychiatristRepository _pRepo;
        private readonly IUserRepository _uRepo;

        public PsychiatristService(IConfiguration configuration, IPsychiatristRepository repo)
        {
            _configuration = configuration;
            _pRepo = repo;
        }

        public async Task<BaseResponseModel<Psychiatrist>> UpdatePsychiatristAsync(PsychiatristRequestModelForUpdate request, int id)
        {
            // Retrieve the Psychiatrist by id
            var existingPsychiatrist = await _pRepo.GetPsychiatristById(id);
            if (existingPsychiatrist == null)
            {
                return new BaseResponseModel<Psychiatrist>
                {
                    Code = 500,
                    Message = "Psychiatrist not exists",
                    Data = null
                };
            }

            // Retrieve the associated User by the foreign key UserId
            var existingUser = await _uRepo.GetUserById(existingPsychiatrist.UserId);
            if (existingUser == null)
            {
                return new BaseResponseModel<Psychiatrist>
                {
                    Code = 500,
                    Message = "Associated User not exists",
                    Data = null
                };
            }           
            existingUser.Fullname = request.Fullname;
            existingUser.Email = request.Email;
            existingUser.DateOfBirth = request.DateOfBirth;
            existingUser.Phonenumber = request.Phonenumber;
            existingUser.Address = request.Address;
            existingUser.Gender = request.Gender;

            existingPsychiatrist.Specialization = request.Specialization;
            existingPsychiatrist.Bio = request.Bio;
            existingPsychiatrist.Experience = request.Experience;
            existingPsychiatrist.Location = request.Location;

            try
            {
                // Update the User and Psychiatrist in the database
                await _uRepo.UpdateUserAsync(existingUser, existingPsychiatrist.UserId);
                await _pRepo.UpdatePsychiatristAsync(existingPsychiatrist, id);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<Psychiatrist>
                {
                    Code = 500,
                    Message = ex.Message,
                    Data = null
                };
            }

            return new BaseResponseModel<Psychiatrist>
            {
                Code = 200,
                Message = "Psychiatrist Updated Successfully",
                Data = existingPsychiatrist
            };
        }
    }
}
