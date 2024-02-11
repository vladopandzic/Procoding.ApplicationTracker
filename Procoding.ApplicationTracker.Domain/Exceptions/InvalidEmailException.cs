using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Exceptions;

/// <summary>
/// Represents exception that occurs when Email in maloformed.
/// </summary>
public sealed class InvalidEmailException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidEmailException"/> class.
    /// </summary>
    public InvalidEmailException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidEmailException"/> class.
    /// </summary>
    public InvalidEmailException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidEmailException"/> class.
    /// </summary>
    public InvalidEmailException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
