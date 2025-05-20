using Microsoft.EntityFrameworkCore;
using Tut11.Data;
using Tut11.DTOs;
using Tut11.Models;

namespace Tut11.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly PrescriptionContext _context;

    public PrescriptionService(PrescriptionContext context)
    {
        _context = context;
    }

    public async Task AddPrescriptionAsync(PrescriptionRequestDto request)
    {
        if (request.Medicaments.Count > 10)
            throw new ArgumentException("Prescription cannot contain more than 10 medicaments.");

        if (request.DueDate < request.Date)
            throw new ArgumentException("DueDate cannot be earlier than Date.");

        var doctor = await _context.Doctors.FindAsync(request.Doctor.IdDoctor);
        if (doctor == null)
            throw new ArgumentException("Doctor not found.");

        var patient = _context.Patients
            .FirstOrDefault(p => p.FirstName == request.Patient.FirstName && p.LastName == request.Patient.LastName && p.BirthDate == request.Patient.BirthDate);

        if (patient == null)
        {
            patient = new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                BirthDate = request.Patient.BirthDate
            };
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        var prescription = new Prescription
        {
            Date = request.Date,
            DueDate = request.DueDate,
            IdDoctor = doctor.IdDoctor,
            IdPatient = patient.IdPatient,
            Prescription_Medicaments = new List<Prescription_Medicament>()
        };

        foreach (var medDto in request.Medicaments)
        {
            var medicament = await _context.Medicaments.FindAsync(medDto.IdMedicament);
            if (medicament == null)
                throw new ArgumentException($"Medicament with ID {medDto.IdMedicament} not found.");

            prescription.Prescription_Medicaments.Add(new Prescription_Medicament
            {
                IdMedicament = medicament.IdMedicament,
                Dose = medDto.Dose,
                Details = medDto.Details
            });
        }

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();
    }

    public async Task<PatientDetailsDto?> GetPatientDetailsAsync(int id)
    {
        {
            var patient = await _context.Patients
                .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Prescription_Medicaments)
                .ThenInclude(pm => pm.Medicament)
                .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Doctor)
                .FirstOrDefaultAsync(p => p.IdPatient == id);

            if (patient == null)
                return null;

            return new PatientDetailsDto
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                BirthDate = patient.BirthDate,
                Prescriptions = patient.Prescriptions
                    .OrderBy(p => p.DueDate)
                    .Select(p => new PrescriptionDetailsDto
                    {
                        IdPrescription = p.IdPrescription,
                        Date = p.Date,
                        DueDate = p.DueDate,
                        Doctor = new DoctorSimpleDto
                        {
                            IdDoctor = p.Doctor.IdDoctor,
                            FirstName = p.Doctor.FirstName,
                            LastName = p.Doctor.LastName
                        },
                        Medicaments = p.Prescription_Medicaments.Select(pm => new MedicamentDetailsDto
                        {
                            IdMedicament = pm.Medicament.IdMedicament,
                            Name = pm.Medicament.Name,
                            Dose = pm.Dose,
                            Details = pm.Details
                        }).ToList()
                    }).ToList()
            };
        }
    }
}