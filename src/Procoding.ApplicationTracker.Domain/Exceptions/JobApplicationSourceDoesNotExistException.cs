namespace Procoding.ApplicationTracker.Domain.Exceptions;

/// <summary>
/// Represents exception that occurs when job application source does not exist.
/// </summary>
public sealed class JobApplicationSourceDoesNotExistException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="JobApplicationSourceDoesNotExistException"/> class.
    /// </summary>
    public JobApplicationSourceDoesNotExistException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JobApplicationSourceDoesNotExistException"/> class.
    /// </summary>
    public JobApplicationSourceDoesNotExistException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JobApplicationSourceDoesNotExistException"/> class.
    /// </summary>
    public JobApplicationSourceDoesNotExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
