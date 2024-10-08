﻿using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ITimeSlotRepository _tsRepo;

        public TimeSlotService(ITimeSlotRepository repo)
        {
            _tsRepo = repo;
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<BaseResponseModel<IEnumerable<TimeSlotResponseModel>>> GetAllTimeSlotsAsync()
        {
            var timeslots = await _tsRepo.GetAllTimeSlotAsync();

            var response = timeslots.Select(ts => new TimeSlotResponseModel
            {
                DateOfWeek = ts.DateOfWeek,
                StartTime = ts.StartTime,
                EndTime = ts.EndTime,
                PsychiatristName = ts.Psychiatrist.User.Fullname,
            }).ToList();

            return new BaseResponseModel<IEnumerable<TimeSlotResponseModel>>
            {
                Code = 200,
                Message = "Get all time slots success!",
                Data = response
            };
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public async Task<TimeSlot?> GetTimeSlotByIdAsync(int id)
        {
            return await _tsRepo.GetTimeSlotByIdAsync(id);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<BaseResponseModel<TimeSlotResponseModel>> AddTimeSlotAsync(TimeSlotRequestModel request, int psychiatristId)
        {
            // Kiểm tra xem psychiatrist có tồn tại không
            var existedPsychiatrist = await _tsRepo.GetTimeSlotByIdAsync(psychiatristId);
            if (existedPsychiatrist == null)
            {
                return new BaseResponseModel<TimeSlotResponseModel>
                {
                    Code = 500,
                    Message = "Psychiatrist not exists!",
                    Data = null
                };
            }

            // Kiểm tra xem có TimeSlot nào trùng không
            var existingTimeSlot = await _tsRepo.GetTimeSlotByDetailsAsync(request.StartTime, request.DateOfWeek, psychiatristId);
            if (existingTimeSlot != null)
            {
                return new BaseResponseModel<TimeSlotResponseModel>
                {
                    Code = 400,
                    Message = "Time slot already exists for this psychiatrist at the specified time and day.",
                    Data = null
                };
            }

            // Tạo đối tượng TimeSlot mới
            var newTimeSlot = new TimeSlot
            {
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                DateOfWeek = request.DateOfWeek,
                PsychiatristId = psychiatristId
            };

            // Thêm TimeSlot vào cơ sở dữ liệu
            await _tsRepo.AddTimeSlotAsync(newTimeSlot);

            return new BaseResponseModel<TimeSlotResponseModel>
            {
                Code = 200,
                Message = "Time slot created successfully.",
                Data = new TimeSlotResponseModel
                {
                    StartTime = newTimeSlot.StartTime,
                    EndTime = newTimeSlot.EndTime,
                    DateOfWeek = newTimeSlot.DateOfWeek
                }
            };
        }

        //-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<BaseResponseModel<TimeSlot>> DeleteTimeSlotAsync(int timeSlotId)
        {
            var timeSlot = await _tsRepo.GetTimeSlotByIdAsync(timeSlotId);  // Lấy đối tượng TimeSlot

            if (timeSlot == null)
            {
                return new BaseResponseModel<TimeSlot>
                {
                    Code = 404,
                    Message = "Time slot not found.",
                    Data = null
                };
            }

            try
            {
                await _tsRepo.DeleteTimeSlotAsync(timeSlot);  // Truyền đối tượng TimeSlot vào thay vì ID
                return new BaseResponseModel<TimeSlot>
                {
                    Code = 200,
                    Message = "Time slot deleted successfully.",
                    Data = timeSlot
                };
            }
            catch (Exception ex)
            {
                return new BaseResponseModel<TimeSlot>
                {
                    Code = 500,
                    Message = "An error occurred while deleting the time slot: " + ex.Message,
                    Data = null
                };
            }
        }
    }
}
