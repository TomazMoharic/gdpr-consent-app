namespace GdprConsentApp.Models;

public class EntityUpdatedResponseModel
{
    /// <summary>
    /// Message of success or failure.
    /// </summary>
    public string Message { get; set; }

    public EntityUpdatedResponseModel(string message)
    {
        Message = message;
    }
}