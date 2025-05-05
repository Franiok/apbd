using Microsoft.Data.SqlClient;
using Tutorial8.Models.DTOs;
using Microsoft.Extensions.Configuration;

namespace Tutorial8.Services;
public class ClientsService : IClientsService
{
    private readonly IConfiguration _configuration;

    public ClientsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<int> CreateClientAsync(ClientDTO dto)
    {
        using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        using var command = new SqlCommand("INSERT INTO Client (FirstName, LastName, Email, Telephone, Pesel) OUTPUT INSERTED.IdClient VALUES (@FirstName, @LastName, @Email, @Telephone, @Pesel)", connection);

        command.Parameters.AddWithValue("@FirstName", dto.FirstName);
        command.Parameters.AddWithValue("@LastName", dto.LastName);
        command.Parameters.AddWithValue("@Email", dto.Email);
        command.Parameters.AddWithValue("@Telephone", dto.Telephone);
        command.Parameters.AddWithValue("@Pesel", dto.Pesel);

        await connection.OpenAsync();
        var newId = (int)await command.ExecuteScalarAsync();
        return newId;
    }

    public async Task<IEnumerable<TripDTO>> GetTripsForClientAsync(int idClient)
    {
        var trips = new List<TripDTO>();
        using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        using var command = new SqlCommand(@"SELECT t.IdTrip, t.Name, t.Description, t.DateFrom, t.DateTo, t.MaxPeople, c.Name AS Country
                                            FROM Trip t
                                            JOIN Client_Trip ct ON ct.IdTrip = t.IdTrip
                                            JOIN Country_Trip crt ON crt.IdTrip = t.IdTrip
                                            JOIN Country c ON c.IdCountry = crt.IdCountry
                                            WHERE ct.IdClient = @IdClient", connection);

        command.Parameters.AddWithValue("@IdClient", idClient);
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

    public async Task<bool> RegisterClientToTripAsync(int idClient, int idTrip)
    {
        using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        await connection.OpenAsync();

        // check if client exists
        using (var checkClient = new SqlCommand("SELECT 1 FROM Client WHERE IdClient = @IdClient", connection))
        {
            checkClient.Parameters.AddWithValue("@IdClient", idClient);
            if ((await checkClient.ExecuteScalarAsync()) == null) return false;
        }

        // check if trip exists
        using (var checkTrip = new SqlCommand("SELECT MaxPeople FROM Trip WHERE IdTrip = @IdTrip", connection))
        {
            checkTrip.Parameters.AddWithValue("@IdTrip", idTrip);
            var maxPeople = (int?)await checkTrip.ExecuteScalarAsync();
            if (maxPeople == null) return false;

            // count current registrations
            using var countCmd = new SqlCommand("SELECT COUNT(*) FROM Client_Trip WHERE IdTrip = @IdTrip", connection);
            countCmd.Parameters.AddWithValue("@IdTrip", idTrip);
            var count = (int)await countCmd.ExecuteScalarAsync();
            if (count >= maxPeople) return false;
        }

        // insert registration
        using var insertCmd = new SqlCommand("INSERT INTO Client_Trip (IdClient, IdTrip, RegisteredAt) VALUES (@IdClient, @IdTrip, GETDATE())", connection);
        insertCmd.Parameters.AddWithValue("@IdClient", idClient);
        insertCmd.Parameters.AddWithValue("@IdTrip", idTrip);

        return await insertCmd.ExecuteNonQueryAsync() > 0;
    }

    public async Task<bool> UnregisterClientFromTripAsync(int idClient, int idTrip)
    {
        using var connection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        await connection.OpenAsync();

        using var command = new SqlCommand("DELETE FROM Client_Trip WHERE IdClient = @IdClient AND IdTrip = @IdTrip", connection);
        command.Parameters.AddWithValue("@IdClient", idClient);
        command.Parameters.AddWithValue("@IdTrip", idTrip);

        return await command.ExecuteNonQueryAsync() > 0;
    }
}