using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Exceptions;

/// <summary>
/// Represents exception that occurs when job application source does not exist.
/// </summary>
public sealed class CompanyDoesNotExistException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CompanyDoesNotExistException"/> class.
    /// </summary>
    public CompanyDoesNotExistException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CompanyDoesNotExistException"/> class.
    /// </summary>
    public CompanyDoesNotExistException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="JobApplicationSourceDoesNotExistException"/> class.
    /// </summary>
    public CompanyDoesNotExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
