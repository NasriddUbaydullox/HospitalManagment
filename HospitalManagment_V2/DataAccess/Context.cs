using HospitalManagment_V2.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagment_V2.DataAccess
{
    public class Context : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientBlank> PatientBlanks { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(builder =>
            {
                builder.HasKey(r => r.Id);

                builder.HasOne(r => r.Patient)
                    .WithMany(r => r.Appointments);

                builder.HasOne(r => r.Doctor)
                    .WithMany(r => r.Appointments);
            });

            modelBuilder.Entity<Doctor>(builder =>
            {
                builder.HasKey(r => r.Id);

                builder.HasOne(r => r.Speciality)
                    .WithMany();
            });

            modelBuilder.Entity<Patient>(builder =>
            {
                builder.HasKey(r => r.Id);
            });

            modelBuilder.Entity<PatientBlank>(builder =>
            {
                builder.HasKey(r => r.Id);

                builder.HasOne(r => r.patient)
                    .WithOne(r => r.PatientBlank)
                    .HasForeignKey<PatientBlank>(r => r.PatientId);
            });

            modelBuilder.Entity<Speciality>(builder =>
            {
                builder.HasKey(r => r.Id);
            });
        }
    }
}
