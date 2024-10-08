﻿using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace WellMeetAPI.Controllers
{
    public class TimeSlotController : Controller
    {
        private readonly ITimeSlotService _tsService;

        public TimeSlotController(ITimeSlotService tsService)
        {
            _tsService = tsService;
        }

        [HttpGet("getalltimeslot")]
        public async Task<ActionResult<BaseResponseModel<IEnumerable<TimeSlotResponseModel>>>> GetAllTimeSlotsController()
        {
            var response = await _tsService.GetAllTimeSlotsAsync();
            return StatusCode((int)response.Code, response);
        }

        [HttpPost("{psychiatristId}")]
        public async Task<IActionResult> AddTimeSlotController([FromBody] TimeSlotRequestModel request, int psychiatristId)
        {
            var response = await _tsService.AddTimeSlotAsync(request, psychiatristId);
            return StatusCode((int)response.Code, response);
        }


        [HttpDelete("deletetimeslot/{id}")]
        public async Task<IActionResult> DeleteTimeSlot(int id)
        {
            var response = await _tsService.DeleteTimeSlotAsync(id);  // Gọi service để xử lý việc xóa TimeSlot
            return StatusCode((int)response.Code, response);
        }

    }
}
