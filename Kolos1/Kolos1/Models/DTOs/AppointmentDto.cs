namespace Kolos1.Models.DTOs;

public class AppointmentDto
{
    public DateTime Date { get; set; }
    public PatientDto? Patient { get; set; }
    public DoctorDto? Doctor { get; set; }
    public AppointmentServicesDto? AppointmentServices { get; set; }
}

public class PatientDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
}

public class DoctorDto
{
    public int DoctorId { get; set; }
    public string Pwz { get; set; } = string.Empty;
}

public class AppointmentServicesDto
{
    public List<AppointmentServiceDto> AppointmentServices { get; set; } = [];
}

public class AppointmentServiceDto
{
    public string Name { get; set; } = string.Empty;
    public int ServiceFee { get; set; }
}