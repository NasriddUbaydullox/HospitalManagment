namespace HospitalManagment_V2.DataAccess.Entities
{
    public class PatientBlank
    {
        public int Id { get; set; }
        public string BlankIdentifier { get; set; }
        public bool IsActive { get; set; }

        public int Severity { get; set; }

        public DateTime CreatedAt { get; set; }
        public int PatientId { get; set; }
        public Patient patient { get; set; }
    }
}
