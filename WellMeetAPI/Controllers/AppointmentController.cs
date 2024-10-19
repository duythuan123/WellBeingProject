using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WellMeetAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {

        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _appointmentService.GetAllAsync();

            return StatusCode((int)result.Code, result);
        }

        [HttpGet("GetAllByUserId/{id}")]
        public async Task<IActionResult> GetAllByUserId(int id)
        {
            var result = await _appointmentService.GetByUserIdAsync(id);

            return StatusCode((int)result.Code, result);
        }


        [HttpGet("GetAllByPsychiatristId/{id}")]
        public async Task<IActionResult> GetAllByPsychiatristId(int id)
        {
            var result = await _appointmentService.GetByPsychiatristIdAsync(id);

            return StatusCode((int)result.Code, result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> create([FromBody] AppointmentRequestModel request)
        {
            var result = await _appointmentService.AddAsync(request);

            return StatusCode((int)result.Code, result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> update([FromBody] AppointmentRequestModelForUpdate request, int id)
        {
            var result = await _appointmentService.UpdateAsync(request, id);

            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            var result = await _appointmentService.DeleteAsync(id);
            return StatusCode((int)result.Code, result);
        }
    }
}
