using HospitalManagment_V2.DataAccess.Entities;

namespace HospitalManagment_V2.Dtos;


public class PatientDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public bool IsActive { get; set; }
    public DateTime RegisteredDate { get; set; }
    public int? PatientBlankId { get; set; }
    public string BlankIdentifier { get; set; }
}

