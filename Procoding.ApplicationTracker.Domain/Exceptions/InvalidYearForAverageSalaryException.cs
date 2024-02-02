namespace Procoding.ApplicationTracker.Domain.Exceptions;

/// <summary>
/// Represents exception that occurs when invalid year is supplied
/// </summary>
public sealed class InvalidYearForAverageSalaryException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidYearForAverageSalaryException"/> class.
    /// </summary>
    public InvalidYearForAverageSalaryException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidYearForAverageSalaryException"/> class.
    /// </summary>
    public InvalidYearForAverageSalaryException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidYearForAverageSalaryException"/> class.
    /// </summary>
    public InvalidYearForAverageSalaryException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
