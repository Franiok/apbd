using Tut11.DTOs;

namespace Tut11.Services;

public interface IPrescriptionService
{
    public Task AddPrescriptionAsync(PrescriptionRequestDto request);
    public Task<PatientDetailsDto?> GetPatientDetailsAsync(int id);
}