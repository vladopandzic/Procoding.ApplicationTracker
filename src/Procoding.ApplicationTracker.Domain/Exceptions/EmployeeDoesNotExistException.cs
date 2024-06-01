using System.ComponentModel.DataAnnotations;

namespace Procoding.ApplicationTracker.Domain.Exceptions;


/// <summary>
/// Represents exception that occurs when job application source does not exist.
/// </summary>
public sealed class EmployeeDoesNotExistException : ValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeDoesNotExistException"/> class.
    /// </summary>
    public EmployeeDoesNotExistException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EmployeeDoesNotExistException"/> class.
    /// </summary>
    public EmployeeDoesNotExistException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JobApplicationSourceDoesNotExistException"/> class.
    /// </summary>
    public EmployeeDoesNotExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}