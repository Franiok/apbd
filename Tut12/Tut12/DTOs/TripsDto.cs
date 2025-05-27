namespace Tut12.DTOs;

public class TripsDto
{
    public int PageNumber {get; set;}
    public int PageSize {get; set;}
    public int AllPages {get; set;}
    public List<TripDto> Trips {get; set;}
}

public class TripDto
{
    public string Name {get; set;}
    public string Desc {get; set;}
    public DateTime DateFrom {get; set;}
    public DateTime DateTo {get; set;}
    public int MaxPeople {get; set;}
    public List<CountryDto> Countries {get; set;}
    public List<ClientDto> Clients {get; set;}
}

public class CountryDto
{
    public string Name {get; set;}
}

public class ClientDto
{
    public string FirstName {get; set;}
    public string LastName {get; set;}
}

public class AssignClientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }   
    public string Telephone { get; set; }
    public string Pesel { get; set; }
    public int IdTrip { get; set; }
    public string TripName { get; set; }
    public DateTime? PaymentDate{ get; set; }
}