using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Exceptions;

/// <summary>
/// Represents exception that occurs when there is 401 need to be returned.
/// </summary>
public sealed class Unauthorized401Exception : ValidationException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Unauthorized401Exception"/> class.
    /// </summary>
    public Unauthorized401Exception()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Unauthorized401Exception"/> class.
    /// </summary>
    public Unauthorized401Exception(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Unauthorized401Exception"/> class.
    /// </summary>
    public Unauthorized401Exception(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
