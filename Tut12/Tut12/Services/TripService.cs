using Microsoft.EntityFrameworkCore;
using Tut12.Data;
using Tut12.DTOs;
using Tut12.Exceptions;
using Tut12.Models;

namespace Tut12.Services;

public class TripService : ITripService
{
    private readonly ApbdContext _context;

    public TripService(ApbdContext context)
    {
        _context = context;
    }

    public async Task<TripsDto> GetTrip(int page, int pageSize)
    {
        var query = _context.Trips
            .Include(t => t.IdCountries)
            .Include(t => t.ClientTrips).ThenInclude(ct => ct.IdClientNavigation)
            .OrderByDescending(t => t.DateFrom);

        var totalItems = await query.CountAsync();
        var trips = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new TripsDto
        {
            PageNumber = page,
            PageSize = pageSize,
            AllPages = (int)Math.Ceiling((double)totalItems / pageSize),
            Trips = trips.Select(t => new TripDto
            {
                Name = t.Name,
                Desc = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.IdCountries.Select(c => new CountryDto { Name = c.Name }).ToList(),
                Clients = t.ClientTrips.Select(c => new ClientDto
                {
                    FirstName = c.IdClientNavigation.FirstName,
                    LastName = c.IdClientNavigation.LastName
                }).ToList()
            }).ToList()
        };
    }

    public async Task AssignClient(AssignClientDto assignClientDto)
    {
        if (await _context.Clients.AnyAsync(c => c.Pesel == assignClientDto.Pesel))
            throw new ClientAlreadyExistsException();

        var trip = await _context.Trips
            .Include(t => t.ClientTrips)
            .FirstOrDefaultAsync(t => t.IdTrip == assignClientDto.IdTrip);

        if (trip == null || trip.DateFrom <= DateTime.Now)
            throw new TripNotFoundException();

        var client = new Client
        {
            FirstName = assignClientDto.FirstName,
            LastName = assignClientDto.LastName,
            Email = assignClientDto.Email,
            Telephone = assignClientDto.Telephone,
            Pesel = assignClientDto.Pesel
        };

        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();

        var clientTrip = new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = assignClientDto.IdTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = assignClientDto.PaymentDate
        };

        await _context.ClientTrips.AddAsync(clientTrip);
        await _context.SaveChangesAsync();
    }
    
}
        