﻿using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WellMeetAPI.Controllers
{
    public class PsychiatristController : Controller
    {
        private readonly IPsychiatristService _psychiatristService;

        public PsychiatristController(IPsychiatristService psychiatristService) 
        {
            _psychiatristService = psychiatristService;

        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetPsychiatristByUserId(int userId)
        {
            var result = await _psychiatristService.GetPsychiatristDetailAsync(userId);

            return StatusCode((int)result.Code, result);
        }

        [HttpPut("update/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdatePsychiatristProfile([FromBody] PsychiatristRequestModelForUpdate request, int id)
        {
            var result = await _psychiatristService.UpdatePsychiatristAsync(request, id);

            return StatusCode((int)result.Code, result);
        }
    }
}