using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tut11.Data;
using Tut11.DTOs;
using Tut11.Services;

namespace Tut11.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionService _service;

        public PrescriptionsController(IPrescriptionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> AddPrescription([FromBody] PrescriptionRequestDto request)
        {
            try
            {
                await _service.AddPrescriptionAsync(request);
                return Ok("Prescription added successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientDetails(int id)
        {
            var result = await _service.GetPatientDetailsAsync(id);
            if (result == null)
                return NotFound("Patient not found.");

            return Ok(result);
        }
    }
}
