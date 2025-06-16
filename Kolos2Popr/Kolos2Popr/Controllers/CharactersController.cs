using Kolos2Popr.Exceptions;
using Kolos2Popr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolos2Popr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly IDbService _dbService;

    public CharactersController(IDbService dbService)
    {
        _dbService = dbService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(int id)
    {
        try
        {
            var order = await _dbService.GetCharacterById(id);
            return Ok(order);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
    
    // [HttpPost]
   // public async Task<IActionResult> Post(int id, List<int> itemIds)
   // {
   // }
}