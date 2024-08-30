using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WellMeetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("create")]
        [Authorize]
        public async Task<IActionResult> create([FromBody] BookingRequestModel request)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "Check JWT token." });
            }
            var userId = int.Parse(userIdClaim.Value);

            var result = await _bookingService.BookAppointmentAsync(userId, request);

            return StatusCode((int)result.Code, result);
        }
    }
}
