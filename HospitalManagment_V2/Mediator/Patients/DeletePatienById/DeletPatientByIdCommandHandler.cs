using HospitalManagment_V2.DataAccess;
using HospitalManagment_V2.Repository;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace HospitalManagment_V2.Mediator.Patients.DeletePatientById;

public class DeletePatientByIdCommandHandler : IRequestHandler<DeletePatientByIdCommand>
{
    private readonly IPatientRepository _repo;

    public DeletePatientByIdCommandHandler(IPatientRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeletePatientByIdCommand request, CancellationToken cancellationToken)
    {
        await _repo.DeleteAsync(request.id);
    }
}