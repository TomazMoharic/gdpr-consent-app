namespace GdprConsentApp.Exceptions;

public class InternalServerException : BaseException
{
    public InternalServerException()
    {
        StatusCode = 500;
    }

    public InternalServerException(string message) : base (message)
    {
        StatusCode = 500;
        // Message = message;
    }
}