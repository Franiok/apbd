using Microsoft.AspNetCore.Mvc;
using Tutorial8.Services;
using Tutorial8.Models.DTOs;

namespace Tutorial8.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientsService _clientsService;

    public ClientsController(IClientsService clientsService)
    {
        _clientsService = clientsService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateClient([FromBody] ClientDTO dto)
    {
        var id = await _clientsService.CreateClientAsync(dto);
        return CreatedAtAction(null, new { id }, dto);
    }

    [HttpGet("{idClient:int}/trips")]
    public async Task<IActionResult> GetClientTrips(int idClient)
    {
        var trips = await _clientsService.GetTripsForClientAsync(idClient);
        return Ok(trips);
    }

    [HttpPost("{idClient:int}/trips/{idTrip:int}")]
    public async Task<IActionResult> RegisterClientToTrip(int idClient, int idTrip)
    {
        var result = await _clientsService.RegisterClientToTripAsync(idClient, idTrip);
        if (!result) return BadRequest("Could not register client to trip.");
        return Ok();
    }

    [HttpDelete("{idClient:int}/trips/{idTrip:int}")]
    public async Task<IActionResult> UnregisterClientFromTrip(int idClient, int idTrip)
    {
        var result = await _clientsService.UnregisterClientFromTripAsync(idClient, idTrip);
        if (!result) return NotFound("Client or trip not found or not registered.");
        return Ok();
    }
}
