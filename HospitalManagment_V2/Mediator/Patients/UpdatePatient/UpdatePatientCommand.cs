using HospitalManagment_V2.Mediator.Patients.UpdatePatient;
using MediatR;

namespace HospitalManagment_V2.Mediator.Patients.UpdatePatientById;

public record UpdatePatientCommand(PatientUpdateDto dto) : IRequest;