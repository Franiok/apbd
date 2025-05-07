using Kolos1.Exceptions;
using Kolos1.Models.DTOs;
using Kolos1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolos1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IDbService _dbService;

    public AppointmentsController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(int id)
    {
        try
        {
            var res = await _dbService.GetAppointmentById(id);
            return Ok(res);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

   // [HttpPost]
   // public async Task<IActionResult> AddNewAppointment()
   // {
   // }
}