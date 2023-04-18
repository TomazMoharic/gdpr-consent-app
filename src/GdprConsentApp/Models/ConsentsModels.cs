namespace GdprConsentApp.Models;

public class ConsentModel
{
    public int IdConsent { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int ConsentDurationDays { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

public class ConsentSimpleModel
{
    public int IdConsent { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public int ConsentDurationDays { get; set; }
}