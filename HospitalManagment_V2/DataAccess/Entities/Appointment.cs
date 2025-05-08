using System.Numerics;

namespace HospitalManagment_V2.DataAccess.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime AppointmentDate { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
    }
}
