using Kolos2.Data;
using Kolos2.DTOs;
using Kolos2.Exceptions;
using Kolos2.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolos2.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<PurchaseDto>GetOrderById(int purchaseId)
    {
        
        var purchase = await _context.Customers.Select(e => new PurchaseDto
        {
            FirstName = e.FirstName,
            LastName = e.LastName,
            Purchases = e.PurchasedTickets.Select(e => new PurchasesDto()
            {
                Date = e.PurchaseDate,
                Price = ,
                Ticket = e.Ticket.Select(e = new TicketDto()
                {
                    
                }),
                Concert = 
            }).ToList()
        }).FirstOrDefaultAsync(e => e.CustomerId == purchaseId);
        
        if (purchase is null)
            throw new NotFoundException();
        
        return purchase;
    }
}
