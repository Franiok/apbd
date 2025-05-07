namespace Kolos1.Models.DTOs;

public class PostAppointmentDto
{
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public string Pwz { get; set; } = String.Empty;
    public List<ServiceDto> Services { get; set; } = [];
}

public class ServiceDto
{
    public string ServiceName { get; set; } = string.Empty;
    public int ServiceFee { get; set; }
}