using Microsoft.Data.SqlClient;
using Tutorial8.Models.DTOs;

namespace Tutorial8.Services;

public class TripsService : ITripsService
{
    private readonly IConfiguration _configuration;

    public TripsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IEnumerable<TripDTO>> GetTripsAsync()
    {
        var trips = new List<TripDTO>();

        using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        using var command = new SqlCommand("SELECT t.IdTrip, t.Name, t.Description, t.DateFrom, t.DateTo, t.MaxPeople, c.Name AS Country FROM Trip t JOIN Country_Trip ct ON ct.IdTrip = t.IdTrip JOIN Country c ON c.IdCountry = ct.IdCountry", connection);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        var tripDict = new Dictionary<int, TripDTO>();
        while (await reader.ReadAsync())
        {
            var id = reader.GetInt32(0);
            if (!tripDict.ContainsKey(id))
            {
                tripDict[id] = new TripDTO
                {
                    IdTrip = id,
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    DateFrom = reader.GetDateTime(3),
                    DateTo = reader.GetDateTime(4),
                    MaxPeople = reader.GetInt32(5),
                    Countries = new List<string>()
                };
            }
            tripDict[id].Countries.Add(reader.GetString(6));
        }

        return tripDict.Values;
    }

    public async Task<TripDTO?> GetTripByIdAsync(int id)
    {
        using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        using var command = new SqlCommand("SELECT t.IdTrip, t.Name, t.Description, t.DateFrom, t.DateTo, t.MaxPeople, c.Name AS Country FROM Trip t JOIN Country_Trip ct ON ct.IdTrip = t.IdTrip JOIN Country c ON c.IdCountry = ct.IdCountry WHERE t.IdTrip = @IdTrip", connection);
        command.Parameters.AddWithValue("@IdTrip", id);

        await connection.OpenAsync();
        using var reader = await command.ExecuteReaderAsync();

        TripDTO? trip = null;
        while (await reader.ReadAsync())
        {
            if (trip == null)
            {
                trip = new TripDTO
                {
                    IdTrip = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Description = reader.GetString(2),
                    DateFrom = reader.GetDateTime(3),
                    DateTo = reader.GetDateTime(4),
                    MaxPeople = reader.GetInt32(5),
                    Countries = new List<string>()
                };
            }
            trip.Countries.Add(reader.GetString(6));
        }

        return trip;
    }
}