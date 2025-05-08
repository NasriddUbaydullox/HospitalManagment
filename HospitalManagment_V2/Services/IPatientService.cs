using AutoMapper;
using AutoMapper.QueryableExtensions;
using HospitalManagment_V2.Dtos;
using HospitalManagment_V2.Repository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagment_V2.Services;

public interface IPatientService
{
    Task<IList<PatientDto>> GetAllPatients();
}

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patient;
    private readonly IMapper _mapper;

    public PatientService(IMapper mapper , IPatientRepository patientRepository)
    {
        _patient = patientRepository;
        _mapper = mapper;
    }
    public async Task<IList<PatientDto>> GetAllPatients()
    {
        
        var patients = _patient.GetAll()
            .AsNoTracking()
            .ProjectTo<PatientDto>(_mapper.ConfigurationProvider)
            .ToList();

        return patients;
    }
}
