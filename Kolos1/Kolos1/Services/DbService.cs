using Kolos1.Exceptions;
using Kolos1.Models.DTOs;
using Microsoft.Data.SqlClient;

namespace Kolos1.Services;

public class DbService : IDbService
{
    private readonly string _connectionString;
    public DbService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APBD;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False") ?? string.Empty;
    }

    public async Task<AppointmentDto> GetAppointmentById(int id)
    {
        var query = @"select p.first_name, p.last_name, p.date_of_birth, date,
                    d.doctor_id, d.PWZ
                    from Appointment a
                    join Patient p on p.patient_id=a.patient_id
                    join Doctor d on d.doctor_id=a.doctor_id
                    join Appointment_Service ss on a.appoitment_id=ss.appoitment_id
                    join Service s on s.service_id=ss.service_id
                    where a.id = @appoitment_id";
        
        await using SqlConnection connection = new SqlConnection(_connectionString);
        await using SqlCommand command = new SqlCommand();
        
        command.Connection = connection;
        command.CommandText = query;
        await connection.OpenAsync();
        
        command.Parameters.AddWithValue("@appoitment_id", id);
        var reader = await command.ExecuteReaderAsync();

        AppointmentDto? appointments = null;

        while (await reader.ReadAsync())
        {
            if (appointments == null)
            {
                appointments = new AppointmentDto
                {
                    Date = reader.GetDateTime(3),
                    Patient = new PatientDto
                    {
                        FirstName = reader.GetString(0),
                        LastName = reader.GetString(1),
                        DateOfBirth = reader.GetDateTime(2)
                    },
                    
                    Doctor = new DoctorDto
                    {
                        DoctorId = reader.GetInt32(3),
                        Pwz = reader.GetString(4),
                    },
                    // AppointmentServices = 
                };
            }
        }

        if (appointments == null)
        {
            throw new NotFoundException("No appointment found");
        }
        
        return appointments;
    }

    public async Task AddAppointment(PostAppointmentDto appointment)
    {
        await using SqlConnection connection = new SqlConnection(_connectionString);
        await using SqlCommand command = new SqlCommand();
    }
}