﻿using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Models.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WellMeetAPI.Controllers
{
    public class PsychiatristController : Controller
    {
        private readonly IPsychiatristService _pService;

        public PsychiatristController(IPsychiatristService psychiatristService) 
        {
            _pService = psychiatristService;

        }

        [HttpGet("getlist")]
        public async Task<ActionResult<BaseResponseModel<IEnumerable<PsychiatristResponseModel>>>> GetAllPsychiatrists()
        {
            var response = await _pService.GetAllPsychiatristsAsync();
            return StatusCode((int)response.Code, response);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPsychiatristByUserId(int userId)
        {
            var result = await _pService.GetPsychiatristDetailAsync(userId);

            return StatusCode((int)result.Code, result);
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdatePsychiatristProfile([FromBody] PsychiatristRequestModelForUpdate request, int userId)
        {
            var result = await _pService.UpdatePsychiatristAsync(request, userId);

            return StatusCode((int)result.Code, result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePsychiatrist([FromBody] PsychiatristRequestModel request)
        {
            var result = await _pService.AddPsychiatristAsync(request);

            return StatusCode((int)result.Code, result);
        }

        [HttpDelete("delete/{userId}")]
        public async Task<IActionResult> DeletePsychiatrist(int userId)
        {
            var result = await _pService.DeletePsychiatristAsync(userId);
            return StatusCode((int)result.Code, result);
        }
    }
}
