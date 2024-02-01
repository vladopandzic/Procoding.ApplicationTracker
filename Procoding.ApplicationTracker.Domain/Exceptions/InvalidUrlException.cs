namespace Procoding.ApplicationTracker.Domain.Exceptions;

public sealed class InvalidUrlException : Exception
{
    public InvalidUrlException()
    {
    }

    public InvalidUrlException(string? message) : base(message)
    {
    }

    public InvalidUrlException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
