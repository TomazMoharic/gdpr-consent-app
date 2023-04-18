namespace GdprConsentApp.Exceptions;

public class EntityNotFoundException : BaseException
{
    public EntityNotFoundException()
    {
        StatusCode = 404;
    }

    public EntityNotFoundException(string message) : base(message)
    {
        // Message = message;
        StatusCode = 404;
    }
}