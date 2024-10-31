using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public async Task<IActionResult> CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = await _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Ok(new { paymentUrl = url });
        }


        [HttpGet("paymentexcute")]
        public async Task<IActionResult> PaymentCallback()
        {
            var response = await _vnPayService.PaymentExecute(Request.Query);

            var result = await _vnPayService.AddPaymentAsync(response);

            if (response.Success)
            {
                return Redirect("http://localhost:3000/payment-confirmation");
            }
            else
            {
                return Redirect("http://localhost:3000/doctor");
            }

        }
    }
}
