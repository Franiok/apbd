using Microsoft.AspNetCore.Mvc;
using Tut12.DTOs;
using Tut12.Exceptions;
using Tut12.Services;

namespace Tut12.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripService _service;

    public TripsController(ITripService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _service.GetTrip(page, pageSize);
        return Ok(result);
    }
    
    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClient([FromBody] AssignClientDto dto)
    {
        try
        {
            await _service.AssignClient(dto);
            return Ok();
        }
        catch (TripException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}