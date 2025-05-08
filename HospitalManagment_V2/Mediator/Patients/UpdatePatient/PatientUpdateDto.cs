namespace HospitalManagment_V2.Mediator.Patients.UpdatePatient;

public class PatientUpdateDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public bool IsActive { get; set; }
    public DateTime RegisteredDate { get; set; }
}