using HospitalManagment_V2.DataAccess.Entities;

namespace HospitalManagment_V2.Dtos;


public class DoctorDto
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public bool IsActive { get; set; }
    public Speciality Speciality { get; set; }
    public List<AppointmentDto> Appointments { get; set; }
}


