namespace GdprConsentApp.RequestModels;

public class GetConsentsRequestModel
{
    public string? Query { get; set; }  
}

public class CreateConsentRequestModel
{
    public string Name { get; set; }
    public string Code { get; set; }
    public int ConsentDurationDays { get; set; }
}

public class UpdateConsentRequestModel
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public int? ConsentDurationDays { get; set; }
}