namespace GdprConsentApp.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException()
    {
        StatusCode = 400;
    }

    public BadRequestException(string message) : base(message)
    {
        // Message = message;
        StatusCode = 400;
    }
}