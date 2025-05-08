using HospitalManagment_V2.DataAccess;
using HospitalManagment_V2.DataAccess.Entities;
using HospitalManagment_V2.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagment_V2.Mediator.Doctors.GetDoctorById;

public class GetDoctorByIdQueryHandler : IRequestHandler<GetDoctorByIdQuery , DoctorDto>
{
    private readonly Context _context;

    public GetDoctorByIdQueryHandler(Context context)
    {
        _context = context;
    }

    public async Task<DoctorDto> Handle(GetDoctorByIdQuery query, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors
            .Include(d => d.Speciality)
            .Include(d => d.Appointments)
            .FirstOrDefaultAsync(d => d.Id == query.Id, cancellationToken);
        if (doctor == null) return null;

        return new DoctorDto
        {
            Id = doctor.Id,
            Firstname = doctor.Firstname,
            Lastname = doctor.Lastname,
            IsActive = doctor.IsActive,
            Speciality = doctor.Speciality,
            Appointments = doctor.Appointments.Select(a => new AppointmentDto
            {
            }).ToList()
        };
    }
}
