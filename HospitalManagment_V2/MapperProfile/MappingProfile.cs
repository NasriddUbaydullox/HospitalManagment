using AutoMapper;
using HospitalManagment_V2.DataAccess.Entities;
using HospitalManagment_V2.Dtos;
namespace HospitalManagment_V2.MapperProfile;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Patient, PatientDto>()
            
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString()))
            .ForMember(dest => dest.BlankIdentifier, opt => opt.MapFrom(src => src.PatientBlank.BlankIdentifier));
        
    }
}
