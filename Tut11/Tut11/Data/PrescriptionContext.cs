using Microsoft.EntityFrameworkCore;
using Tut11.Models;

namespace Tut11.Data;

public class PrescriptionContext : DbContext
{
    public PrescriptionContext(DbContextOptions options) : base(options) {}

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<Prescription_Medicament> PrescriptionMedicaments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(a =>
        {
            a.ToTable("Doctor");
            
            a.HasKey(e => e.IdDoctor);
            a.Property(e => e.FirstName);
            a.Property(e => e.LastName);
            a.Property(e => e.Email);
        });
        
        modelBuilder.Entity<Patient>(a =>
        {
            a.ToTable("Patient");
            
            a.HasKey(e => e.IdPatient);
            a.Property(e => e.FirstName);
            a.Property(e => e.LastName);
            a.Property(e => e.BirthDate);
        });
        
        modelBuilder.Entity<Medicament>(a =>
        {
            a.ToTable("Medicament");
            
            a.HasKey(e => e.IdMedicament);
            a.Property(e => e.Name);
            a.Property(e => e.Description);
            a.Property(e => e.Type);
        });
        
        modelBuilder.Entity<Prescription>(a =>
        {
            a.ToTable("Prescription");
            
            a.HasKey(e => e.IdPrescription);
            a.Property(e => e.Date);
            a.Property(e => e.DueDate);
            a.Property(e => e.IdPatient);
            a.Property(e => e.IdPrescription);
        });
        
        modelBuilder.Entity<Prescription_Medicament>()
            .HasKey(pm => new { pm.IdMedicament, pm.IdPrescription });

        modelBuilder.Entity<Prescription_Medicament>()
            .HasOne(pm => pm.Medicament)
            .WithMany(m => m.Prescription_Medicaments)
            .HasForeignKey(pm => pm.IdMedicament);

        modelBuilder.Entity<Prescription_Medicament>()
            .HasOne(pm => pm.Prescription)
            .WithMany(p => p.Prescription_Medicaments)
            .HasForeignKey(pm => pm.IdPrescription);
    }
}