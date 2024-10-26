using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using DataAccessLayer.Entities;
using DataAccessLayer.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WellMeetAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel request)
        {
            var result = await _userService.LoginAsync(request);

            return StatusCode((int)result.Code, result);
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllUsersAsync();

            return StatusCode((int)result.Code, result);
        }

        [HttpGet("GetBy/{id}")]
        [Authorize]
        public async Task<IActionResult> getDetail(int id)
        {
            var result = await _userService.GetDetailAsync(id);

            return StatusCode((int)result.Code, result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> create([FromBody] UserRequestModel request)
        {
            var result = await _userService.AddAsync(request);

            return StatusCode((int)result.Code, result);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> update([FromBody] UserRequestModelForUpdate request, int id)
        {
            var result = await _userService.UpdateAsync(request,id);

            return StatusCode((int)result.Code, result);
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> forgotPassword([FromBody] UserRequestModelForForgotPassword request)
        {
            var result = await _userService.ForgotPasswordAsync(request.Email);
            return StatusCode((int)result.Code, result);
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> resetPassword([FromBody] UserRequestModelForChangePassword request)
        {
            var result = await _userService.ResetPasswordAsync(request.Token, request.NewPassword);
            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> delete(int id)
        {
            var result = await _userService.DeleteAsync(id);
            return StatusCode((int)result.Code, result);
        }
    }
}
