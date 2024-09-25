using BusinessLayer.IServices;
using BusinessLayer.Models.Request;
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

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePsychiatristProfile([FromBody] PsychiatristRequestModelForUpdate request, int id)
        {
            var result = await _psychiatristService.UpdatePsychiatristAsync(request, id);

            return StatusCode((int)result.Code, result);
        }
    }
}
