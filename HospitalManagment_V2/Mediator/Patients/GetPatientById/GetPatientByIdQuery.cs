using HospitalManagment_V2.DataAccess.Entities;
using MediatR;

namespace HospitalManagment_V2.Mediator.Patients.GetPatientById;

public record GetPatientByIdQuery(int id) : IRequest<Patient?>;
