namespace HospitalManagment_V2.JwtSetting;

public class JwtSettings
{
    public string Key { get; set; }

    public string[] Issuers { get; set; }

    public string[] Audiences { get; set; }
}
