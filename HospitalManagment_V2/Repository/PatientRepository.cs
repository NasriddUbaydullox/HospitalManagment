using HospitalManagment_V2.DataAccess;
using HospitalManagment_V2.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace HospitalManagment_V2.Repository;

public interface IPatientRepository
{
    IQueryable<Patient> GetAll();

    Task<Patient> GetByIdAsync(int id);

    Task AddAsync(Patient patient);

    Task UpdateAsync(Patient patient);

    Task DeleteAsync(int id);
}

public class PatientRepository : IPatientRepository
{
    private readonly Context _context;
    private readonly IDistributedCache _cache;

    public PatientRepository(Context context , IDistributedCache cache)
    {
        _context = context;
        _cache = cache;
    }

    public IQueryable<Patient> GetAll()
    {
        return _context.Patients
            .AsQueryable();
    }

    public async Task<Patient> GetByIdAsync(int id)
    {
        var cacheDoctor = await _cache.GetStringAsync(id.ToString());
        if(cacheDoctor is not null)
        {
            return JsonSerializer.Deserialize<Patient>(cacheDoctor);
        }

        var doctor = await _context.Patients.FindAsync(id);

        if(doctor is not null)
        {
            var serialized = JsonSerializer.Serialize(doctor);

            await _cache.SetStringAsync(id.ToString(), serialized);
        }

        return doctor;
    }

    public async Task AddAsync(Patient patient)
    {
        await _context.AddAsync(patient);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Patient patient)
    {
        _context.Update(patient);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync (int id)
    {
        var patient = await _context.Patients.FindAsync(id);
        if(patient != null)
        {
            _context.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}
