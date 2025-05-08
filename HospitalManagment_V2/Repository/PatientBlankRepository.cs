using HospitalManagment_V2.DataAccess;
using HospitalManagment_V2.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagment_V2.Repository;

public interface IPatientBlankRepository
{
    Task<IEnumerable<PatientBlank>> GetAllAsync();

    Task<PatientBlank> GetByIdAsync(int id);

    Task AddAsync(PatientBlank patientBlank);

    Task UpdateAsync(PatientBlank patientBlank);

    Task DeleteAsync(int id);
}

public class PatientBlankRepository : IPatientBlankRepository
{
    private readonly Context _context;

    public PatientBlankRepository(Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PatientBlank>> GetAllAsync()
    {
        return await _context.PatientBlanks
            .Include(r=>r.patient)
            .ToListAsync();
    }

    public async Task<PatientBlank> GetByIdAsync(int id)
    {
        return await _context.PatientBlanks.FirstOrDefaultAsync(r=>r.Id == id);
    }

    public async Task AddAsync(PatientBlank pateintBlank)
    {
        await _context.PatientBlanks.AddAsync(pateintBlank);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PatientBlank patient)
    {
        _context.PatientBlanks.Update(patient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var patient = await _context.PatientBlanks.FindAsync(id);
        if(patient != null)
        {
            _context.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}
