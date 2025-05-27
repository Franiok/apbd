using Microsoft.AspNetCore.Mvc;
using Tut12.Exceptions;
using Tut12.Services;

namespace Tut12.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _service;

    public ClientsController(IClientService service)
    {
        _service = service;
    }
    
    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        try
        {
            await _service.DeleteClient(idClient);
            return NoContent();
        }
        catch (ClientNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ClientHasTripsException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}