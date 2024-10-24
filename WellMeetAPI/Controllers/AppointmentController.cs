using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
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
        private readonly IVnPayService _vnPayService;

        public AppointmentController(IAppointmentService appointmentService, IVnPayService vnPayService)
        {
            _appointmentService = appointmentService;
            _vnPayService = vnPayService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _appointmentService.GetAllAsync();

            return StatusCode((int)result.Code, result);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _appointmentService.GetByIdAsync(id);

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

        [HttpPut("FinishAppointment/{id}")]
        public async Task<IActionResult> finsih(int id)
        {
            var result = await _appointmentService.FinishAppointmentAsync(id);

            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            var result = await _appointmentService.DeleteAsync(id);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost("pay")]
        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Ok(url);
        }
        [HttpGet("paymentexcute")]
        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Ok(response);
        }

        [HttpPost("addPayment")]
        public async Task<IActionResult> createPayment(PaymentResponseModel request)
        {
            var result = await _vnPayService.AddPaymentAsync(request);

            return StatusCode((int)result.Code, result);
        }
    }
}
