using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HospitalManagment_V2.DataAccess.Entities;
using HospitalManagment_V2.DataAccess;
using Microsoft.Extensions.Caching.Memory;
using Sats.PostgresDistributedCache;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace HospitalManagment_V2.Repository;

public interface IDoctorRepository
{
    Task<IEnumerable<Doctor>> GetAllAsync();

    Task<Doctor> GetByIdAsync(int id);

    Task AddAsync(Doctor doctor);

    Task UpdateAsync(Doctor doctor);

    Task DeleteAsync(int id);
}

public class DoctorRepository : IDoctorRepository
{
    private readonly Context _context;
    private static Dictionary<int, Doctor> _doctorsCache = new();
    private readonly IMemoryCache _memoryCache;
    //private readonly IPostgreSqlDistributedCache _distrCache;
    private readonly IDistributedCache _distrCache2;

    public DoctorRepository(Context context,IMemoryCache cache ,/* IPostgreSqlDistributedCache postgreSqlDistributedCache ,*/ IDistributedCache distributed)
    {

        _context = context;
        _memoryCache = cache;
        //_distrCache = postgreSqlDistributedCache;
        _distrCache2 = distributed;
    }

    public async Task<IEnumerable<Doctor>> GetAllAsync()
    {
        return await _context.Doctors.Include(d => d.Speciality).ToListAsync();
    }

    public async Task<Doctor> GetByIdAsync(int id)
    {

        #region MemoryCache
        _memoryCache.TryGetValue<Doctor>(id, out var cacheDoctor);

        if (cacheDoctor is not null)
        {
            return cacheDoctor;
        }

        var doctor = await _context.Doctors.FindAsync(id);

        _memoryCache.Set(id, doctor, DateTimeOffset.FromUnixTimeSeconds(10));

        return doctor;
        #endregion 

        #region Cache
        //_doctorsCache.TryGetValue(id, out var doctorCache);
        //if(doctorCache is not null)
        //{
        //    return doctorCache;
        //}

        //var doctor =  await _context.Doctors.FindAsync(id);

        //_doctorsCache.Add(id, doctor);

        //return doctor;
        #endregion
    }

    public async Task AddAsync(Doctor doctor)
    {
        await _context.Doctors.AddAsync(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Doctor doctor)
    {
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var doctor = await _context.Doctors.FindAsync(id);
        if (doctor != null)
        {
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
