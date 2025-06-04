using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolos2.Models;

[Table("Ticket_Concert")]
public class TicketConcert
{
    [Key]
    public int TicketConcertId { get; set; }
    [ForeignKey(nameof(Ticket))]
    public int TicketId { get; set; }
    [ForeignKey(nameof(Concert))]
    public int ConcertId { get; set; }
    [Column(TypeName = "decimal")]
    [Precision(10, 2)]
    public double Price { get; set; }
    
    public Ticket Ticket { get; set; } = null!;
    public Concert Concert { get; set; }  = null!;
    
    public ICollection<PurchasedTicket> PurchasedTickets { get; set; } = null!;

}