using HospitalManagment_V2.Dtos;
using MediatR;

namespace HospitalManagment_V2.Mediator.Doctors.GetDoctorById;

public class GetDoctorByIdQuery : IRequest<DoctorDto>
{
    public int Id { get; set; }
    public GetDoctorByIdQuery(int id) => Id = id;
}