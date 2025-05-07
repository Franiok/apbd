using Kolos1.Models.DTOs;

namespace Kolos1.Services;

public interface IDbService
{
    Task<AppointmentDto> GetAppointmentById(int customerId);
    Task AddAppointment(PostAppointmentDto appointment);
}