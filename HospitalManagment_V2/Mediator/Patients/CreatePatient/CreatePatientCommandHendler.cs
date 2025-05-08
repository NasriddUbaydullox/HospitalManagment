using HospitalManagment_V2.DataAccess;
using HospitalManagment_V2.DataAccess.Entities;
using HospitalManagment_V2.Dtos;
using MediatR;

namespace HospitalManagment_V2.Mediator.Patients.CreatePatient;

public class CreatePatientCommandHendler : IRequestHandler<CreatePatientCommand, int>
{
    private readonly Context _context;

    public CreatePatientCommandHendler(Context context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = new Patient
        {
            FirstName = request.patient.FirstName,
            LastName = request.patient.LastName,
            DateOfBirth = request.patient.DateOfBirth,
            IsActive = request.patient.IsActive,
            RegisteredDate = request.patient.RegisteredDate,
            PatientBlankId = request.patient.PatientBlankId
        };
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();

        return patient.Id;

    }
}
