using HospitalManagment_V2.DataAccess;
using HospitalManagment_V2.DataAccess.Entities;
using MediatR;

namespace HospitalManagment_V2.Mediator.Doctors.CreateDoctor;

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand,int>
{
    private readonly Context _context;

    public CreateDoctorCommandHandler(Context context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateDoctorCommand request , CancellationToken cancellationToken)
    {
        var dto = request.dto;

        // Foreign key uchun id ni olish
        var specialityId = dto.Speciality?.Id
            ?? throw new ArgumentException("SpecialityId kerak");

        var doctor = new Doctor
        {
            Firstname = dto.Firstname,
            Lastname = dto.Lastname,
            IsActive = dto.IsActive,
            SpecialityId = specialityId
        };

        _context.Doctors.Add(doctor);
        await _context.SaveChangesAsync(cancellationToken);
        return doctor.Id;   
    }
}
