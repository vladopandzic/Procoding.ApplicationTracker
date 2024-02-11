using Procoding.ApplicationTracker.Domain.Common;
using Procoding.ApplicationTracker.Domain.Exceptions;

namespace Procoding.ApplicationTracker.Domain.ValueObjects;

/// <summary>
/// Represents email value object.
/// </summary>
public class Email : ValueObject
{
    /// <summary>
    /// Max length email can have.
    /// </summary>
    public static readonly int MaxLengthForValue = 512;

    /// <summary>
    /// Creates new instance of the <see cref="Email"/>. Required by EF Core.
    /// </summary>
#pragma warning disable CS8618
    private Email()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// Creates new instance of the <see cref="Email"/>
    /// </summary>
    public Email(string email)
    {
        if (email.Length > MaxLengthForValue)
        {
            throw new ArgumentException($"Name can not be longer than {email.Length} characters");
        }
        //TODO: add better logic here
        if (!email.Contains("@"))
        {
            throw new InvalidEmailException($"Invalid email");

        }
        Value = email;
    }

    /// <summary>
    /// Actual value for the email.
    /// </summary>
    public string Value { get; }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
