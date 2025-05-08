using MediatR;

namespace HospitalManagment_V2.Mediator.Patients.DeletePatientById;

public record DeletePatientByIdCommand(int id) : IRequest;
