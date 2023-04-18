namespace GdprConsentApp.Exceptions;

public class BaseException : Exception
{
    // public string Message { get; set; }
    public int StatusCode { get; set; }
    
    public BaseException () {}

    public BaseException(string message) : base(message)
    {
        // this._message = message;
    }
    
}