using Microsoft.EntityFrameworkCore;
using Tut12.Data;
using Tut12.Exceptions;

namespace Tut12.Services;

public class ClientService : IClientService
{
    private readonly ApbdContext _context;

    public ClientService(ApbdContext context)
    {
        _context = context;
    }
    public async Task DeleteClient(int idClient)
    {
        
        var client = await _context.Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);

        if (client == null)
            throw new ClientNotFoundException();

        if (client.ClientTrips.Any())
            throw new ClientHasTripsException();

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }
}