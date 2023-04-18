namespace GdprConsentApp.Models;

public class EntityUpdatedResponseModel
{
    public string Message { get; set; }

    public EntityUpdatedResponseModel(string message)
    {
        Message = message;
    }
}