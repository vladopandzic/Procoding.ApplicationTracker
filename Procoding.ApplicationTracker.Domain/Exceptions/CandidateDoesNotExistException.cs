using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Exceptions;

/// <summary>
/// Represents exception that occurs when candidate does not exist.
/// </summary>
public sealed class CandidateDoesNotExistException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CandidateDoesNotExistException"/> class.
    /// </summary>
    public CandidateDoesNotExistException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CandidateDoesNotExistException"/> class.
    /// </summary>
    public CandidateDoesNotExistException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CandidateDoesNotExistException"/> class.
    /// </summary>
    public CandidateDoesNotExistException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
