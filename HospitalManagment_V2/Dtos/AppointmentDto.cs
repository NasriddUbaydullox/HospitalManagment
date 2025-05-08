using HospitalManagment_V2.DataAccess.Entities;

namespace HospitalManagment_V2.Dtos;

public class AppointmentDto
{
    public bool IsActive { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }

    public Appointment ToAppointment()
    {
        return new Appointment()
        {
            IsActive = this.IsActive,
            PatientId = this.PatientId,
            AppointmentDate = this.AppointmentDate,
            DoctorId = this.DoctorId
        };
    }
}
