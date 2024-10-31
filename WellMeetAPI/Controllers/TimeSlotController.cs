using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
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

        [HttpGet("gettimeslotbyid/{id}")]
        public async Task<ActionResult<BaseResponseModel<TimeSlotResponseModel>>> GetTimeSlotByIdController(int id)
        {
            var response = await _tsService.GetTimeSlotByIdAsync(id);  // Gọi service để lấy TimeSlot theo ID
            return StatusCode((int)response.Code, response);
        }

        [HttpPost("{psychiatristId}")]
        public async Task<IActionResult> AddTimeSlotController([FromBody] AddTimeSlotRequestModel request, int psychiatristId)
        {
            var response = await _tsService.AddTimeSlotAsync(request, psychiatristId);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut("updatetimeslot/{id}")]
        public async Task<IActionResult> UpdateTimeSlotController([FromBody] TimeSlotRequestModel request, int id)
        {
            var response = await _tsService.UpdateTimeSlotAsync(request, id);
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
