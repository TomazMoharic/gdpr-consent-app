namespace GdprConsentApp.Models;

public class EntityCreatedResponseModel
{
    /// <summary>
    /// Id of the entity that was created in the database.
    /// </summary>
    public int IdCreatedEntity { get; set; }
    
    /// <summary>
    /// Message of success or failure.
    /// </summary>
    public string Message { get; set; }

    public EntityCreatedResponseModel(int idCreatedEntity, string message)
    {
        IdCreatedEntity = idCreatedEntity;
        Message = message;
    }
}