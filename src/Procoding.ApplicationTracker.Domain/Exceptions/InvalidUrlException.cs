using System.ComponentModel.DataAnnotations;

namespace Procoding.ApplicationTracker.Domain.Exceptions;


/// <summary>
/// Represents exception that occurs when Url in maloformed.
/// </summary>
public sealed class InvalidUrlException : ValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUrlException"/> class.
    /// </summary>
    public InvalidUrlException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUrlException"/> class.
    /// </summary>
    public InvalidUrlException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidUrlException"/> class.
    /// </summary>
    public InvalidUrlException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
