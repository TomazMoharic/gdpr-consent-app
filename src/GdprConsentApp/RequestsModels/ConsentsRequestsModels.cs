namespace GdprConsentApp.RequestModels;

public class GetConsentsRequestModel
{
    /// <summary>
    /// User input query, used to filter the consents. It is compared again Name of the consent.
    /// </summary>
    public string? Query { get; set; }  
}

public class CreateConsentRequestModel
{
    /// <summary>
    /// Name of the consent.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Unique string code of the consent. This can be used to search for specific consent.
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// Duration of the given consent in days.
    /// </summary>
    public int ConsentDurationDays { get; set; }
}

public class UpdateConsentRequestModel
{
    /// <summary>
    /// Name of the consent.
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Unique string code of the consent. This can be used to search for specific consent.
    /// </summary>
    public string? Code { get; set; }
    
    /// <summary>
    /// Duration of the given consent in days.
    /// </summary>
    public int? ConsentDurationDays { get; set; }
}