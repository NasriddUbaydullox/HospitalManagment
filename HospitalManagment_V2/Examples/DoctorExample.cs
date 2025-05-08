using HospitalManagment_V2.DataAccess.Entities;
using HospitalManagment_V2.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace HospitalManagment_V2.Examples;

public class DoctorExample : IExamplesProvider<DoctorDto>
{
    public DoctorDto GetExamples()
    {
        return new DoctorDto()
        {
            Firstname = "John",
            Lastname = "Doe",
            IsActive = true,
            Speciality = new Speciality
            {
                Id = 1,
                Name = "Not good"
            },
            Appointments = new List<AppointmentDto>
            {
                new AppointmentDto
                {
                       IsActive = true,
                       PatientId = 1,
                       DoctorId = 1,
                       AppointmentDate = DateTime.Now,
                }
            }

        };
    }

}
