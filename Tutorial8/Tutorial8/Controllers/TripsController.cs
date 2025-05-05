using Microsoft.AspNetCore.Mvc;
using Tutorial8.Models.DTOs;
using Tutorial8.Services;

namespace Tutorial8.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TripsController : ControllerBase
    {
        private readonly ITripsService _tripsService;

        public TripsController(ITripsService tripsService)
        {
            _tripsService = tripsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrips()
        {
            var trips = await _tripsService.GetTripsAsync();
            return Ok(trips);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTrip(int id)
        {
            var trip = await _tripsService.GetTripByIdAsync(id);
            if (trip is null) return NotFound();
            return Ok(trip);
        }
    }
}