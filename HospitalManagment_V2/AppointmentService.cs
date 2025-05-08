using HospitalManagment_V2.classes;
using HospitalManagment_V2.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HospitalManagment_V2;

public class AppointmentService
{
    private readonly Context _context;
    private readonly IOptions<AppointmentSettings> _options;

    public AppointmentService(Context context , IOptions<AppointmentSettings> options)
    {
        _context = context;
        _options = options;
    }
    
}
