namespace GdprConsentApp.Models;

public class EntityDeletedResponseModel
{
    public string Message { get; set; }

    public EntityDeletedResponseModel(string message)
    {
        Message = message;
    }
}