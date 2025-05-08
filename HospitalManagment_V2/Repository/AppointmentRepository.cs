using HospitalManagment_V2.DataAccess;
using HospitalManagment_V2.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagment_V2.Repository;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAsync();

    Task<Appointment> GetByIdAsync(int id);

    Task AddAsync(Appointment appointment);

    Task UpdateAsync(Appointment appointment);

    Task DeleteAsync(int id);
}

public class AppointmentRepository : IAppointmentRepository
{
    private readonly Context _context;
    public AppointmentRepository(Context context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Appointment>> GetAllAsync()
    {
        return await _context.Appointments.ToListAsync();
    }

    public async Task<Appointment> GetByIdAsync(int id)
    {
        return await _context.Appointments.FirstOrDefaultAsync(r=>r.Id==id);
    }

    public async Task AddAsync(Appointment appointment)
    {
        await _context.AddAsync(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        _context.Update(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if(appointment != null)
        {
            _context.Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }
}
