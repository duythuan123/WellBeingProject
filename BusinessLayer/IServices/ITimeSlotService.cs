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
    public interface ITimeSlotService
    {
        Task<BaseResponseModel<IEnumerable<TimeSlotResponseModel>>> GetAllTimeSlotsAsync();
        Task<BaseResponseModel<TimeSlotModel>> GetTimeSlotByIdAsync(int id);
        Task<BaseResponseModel<TimeSlotResponseModel>> AddTimeSlotAsync(TimeSlotRequestModel request, int psychiatristId);
        Task<BaseResponseModel<TimeSlotResponseModel>> UpdateTimeSlotAsync(TimeSlotRequestModel request, int timeSlotId);
        Task<BaseResponseModel<TimeSlot>> DeleteTimeSlotAsync(int timeSlotId);

    }
}
