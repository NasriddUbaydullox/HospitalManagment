using HospitalManagment_V2.DataAccess;
using HospitalManagment_V2.DataAccess.Entities;
using HospitalManagment_V2.Repository;
using MediatR;

namespace HospitalManagment_V2.Mediator.Patients.UpdatePatientById;

public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand>
{
    private readonly IPatientRepository _repo;

    public UpdatePatientCommandHandler(IPatientRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
    {
        var patient = new Patient
        {
            Id = request.dto.Id,
            FirstName = request.dto.FirstName,
            LastName = request.dto.LastName,
            DateOfBirth = request.dto.DateOfBirth,
            IsActive = request.dto.IsActive,
            RegisteredDate = request.dto.RegisteredDate
        };

        await _repo.UpdateAsync(patient);

    }
}