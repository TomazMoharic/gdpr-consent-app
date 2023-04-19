namespace GdprConsentApp.Models;

public class EntityDeletedResponseModel
{
    /// <summary>
    /// Message of success or failure.
    /// </summary>
    public string Message { get; set; }

    public EntityDeletedResponseModel(string message)
    {
        Message = message;
    }
}