using HospitalManagment_V2.Dtos;
using MediatR;
namespace HospitalManagment_V2.Mediator.Doctors.CreateDoctor;

public class CreateDoctorCommand : IRequest<int>
{
    public DoctorDto dto { get; set; }

    public CreateDoctorCommand(DoctorDto doctor)
    {
        dto = doctor;
    }
}
