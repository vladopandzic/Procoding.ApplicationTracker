using System.ComponentModel.DataAnnotations;

namespace Procoding.ApplicationTracker.Domain.Exceptions;

/// <summary>
/// Represents exception that occurs when job application does not exist.
/// </summary>
public sealed class JobApplicationDoesNotExistException : ValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JobApplicationDoesNotExistException"/> class.
    /// </summary>
    public JobApplicationDoesNotExistException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JobApplicationDoesNotExistException"/> class.
    /// </summary>
    public JobApplicationDoesNotExistException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JobApplicationDoesNotExistException"/> class.
    /// </summary>
    public JobApplicationDoesNotExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
