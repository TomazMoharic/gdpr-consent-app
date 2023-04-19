namespace GdprConsentApp.Models;

/// <summary>
/// Most detailed consent model.
/// </summary>
public class ConsentModel
{
    /// <summary>
    /// Unique numeric identifier of the consent.
    /// </summary>
    public int IdConsent { get; set; }
    
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
    
    /// <summary>
    /// Timestamp of the moment when the consent was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Timestamp of the moment when the consent was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}

/// <summary>
/// Summary consent model used when additional information is not required.
/// </summary>
public class ConsentSimpleModel
{
    /// <summary>
    /// Unique numeric identifier of the consent.
    /// </summary>
    public int IdConsent { get; set; }
        
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