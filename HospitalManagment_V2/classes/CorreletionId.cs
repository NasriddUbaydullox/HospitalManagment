namespace HospitalManagment_V2.classes;

public class CorreletionId : ICorroletionId
{
    private string _correletionId = Guid.NewGuid().ToString();
    public string Get() => _correletionId;

    public void Set(string correletionId)
    {
        correletionId = _correletionId.ToString();  
    }
}
