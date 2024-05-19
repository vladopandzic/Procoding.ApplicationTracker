using Procoding.ApplicationTracker.Domain.Common;
using Procoding.ApplicationTracker.Domain.Exceptions;

namespace Procoding.ApplicationTracker.Domain.ValueObjects;

/// <summary>
/// Represents Link value object.
/// </summary>
public sealed class Link : ValueObject
{
    /// <summary>
    /// Represents maximum length for the link.
    /// </summary>
    public static readonly int MaxLengthForValue = 512;

    /// <summary>
    /// Creates new instance of the <see cref="Link"/> class.
    /// </summary>
#pragma warning disable CS8618
    private Link()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// Creates new instance of the <see cref="Link"/> class. Must be valid url and have maximum length of <see
    /// cref="MaxLengthForValue"/>.
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="InvalidUrlException"></exception>
    public Link(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        if(value.Length > MaxLengthForValue)
        {
            throw new ArgumentException($"Name can not be longer than {value} characters");
        }

        if(!Uri.TryCreate(value, UriKind.Absolute, out var uri) || !uri.IsWellFormedOriginalString())
        {
            throw new InvalidUrlException("Supplied value is not valid url");
        }
        Value = value;
    }

    /// <summary>
    /// Actual link value.
    /// </summary>
    public string Value { get; set; }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
