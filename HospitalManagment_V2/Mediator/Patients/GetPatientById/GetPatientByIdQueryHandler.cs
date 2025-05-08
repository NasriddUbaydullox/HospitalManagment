using HospitalManagment_V2.DataAccess;
using HospitalManagment_V2.DataAccess.Entities;
using MediatR;

namespace HospitalManagment_V2.Mediator.Patients.GetPatientById;

public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Patient?>
{
    private readonly Context _context;

    public GetPatientByIdQueryHandler(Context context)
    {
        _context = context;
    }
    public async Task<Patient?> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients.FindAsync(request.id);
        return patient;
    }
}
