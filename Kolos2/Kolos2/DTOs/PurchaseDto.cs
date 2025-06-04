namespace Kolos2.DTOs;

public class PurchaseDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int? PhoneNumber { get; set; }
    public List<PurchasesDto> Purchases { get; set; } = null!;
}

public class PurchasesDto
{
    public DateTime Date { get; set; }
    public double Price { get; set; }
    public TicketDto Ticket { get; set; } = null!;
    public ConcertDto Concert { get; set; } = null!;
}

public class TicketDto
{
    public string Serial { get; set; } = null!;
    public string SeatNumber { get; set; } = null!;
}

public class ConcertDto
{
    public string Name { get; set; } = null!;
    public DateTime Date { get; set; }
}