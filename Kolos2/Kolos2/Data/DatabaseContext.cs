using System.Data;
using Kolos2.Models;
using Microsoft.EntityFrameworkCore;
namespace Kolos2.Data;

public class DatabaseContext : DbContext
{    
    public DbSet<Concert> Concerts { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<PurchasedTicket> PurchasedTickets { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketConcert> TicketConcerts { get; set; }
    
    protected DatabaseContext()
    {
    }
    
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Concert>().HasData(new List<Concert>()
        {
            new Concert() { ConcertId = 1, Name = "Concert 1", Date = DateTime.Parse("2021-01-01") },
        });
        
        modelBuilder.Entity<Customer>().HasData(new List<Customer>()
        {
            new Customer() {CustomerId = 1, FirstName = "John", LastName = "Doe"}
        });
        
        modelBuilder.Entity<Ticket>().HasData(new List<Ticket>()
        {
            new Ticket() {TicketId = 1, SerialNumber = "123", SeatNumber = 1234}
        });
        
        modelBuilder.Entity<TicketConcert>().HasData(new List<TicketConcert>()
        {
            new TicketConcert() {TicketConcertId = 1, TicketId = 1, ConcertId = 1, Price = 2.3}
        });
        
        modelBuilder.Entity<PurchasedTicket>().HasData(new List<PurchasedTicket>()
        {
            new PurchasedTicket() {TicketConcertId = 1, CustomerId = 1, PurchaseDate = DateTime.Parse("2021-01-01")}
        });
    }
}