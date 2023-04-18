namespace GdprConsentApp.Models;

public class EntityCreatedResponseModel
{
    public int IdCreatedEntity { get; set; }
    public string Message { get; set; }

    public EntityCreatedResponseModel(int idCreatedEntity, string message)
    {
        IdCreatedEntity = idCreatedEntity;
        Message = message;
    }
}