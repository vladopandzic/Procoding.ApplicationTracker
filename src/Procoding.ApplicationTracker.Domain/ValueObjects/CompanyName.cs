using Procoding.ApplicationTracker.Domain.Common;

namespace Procoding.ApplicationTracker.Domain.ValueObjects;

/// <summary>
/// Name for the <see cref="Entities.Company"/>
/// </summary>
public sealed class CompanyName : ValueObject
{
    /// <summary>
    /// Max length for the company name
    /// </summary>
    public static readonly int MaxLengthForName = 255;

    /// <summary>
    /// Initalizes new instance of the <see cref="CompanyName"/>.
    /// </summary>
#pragma warning disable CS8618
    private CompanyName()
    {
    }
#pragma warning restore CS8618

    /// <summary>
    /// Creates new instance of the <see cref="CompanyName"/>.
    /// </summary>
    /// <param name="name"></param>
    /// <exception cref="ArgumentException"></exception>
    public CompanyName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        if(name.Length > MaxLengthForName)
        {
            throw new ArgumentException($"Name can not be longer than {MaxLengthForName} characters");
        }
        Value = name;
    }

    /// <summary>
    /// Actual value for the company.
    /// </summary>
    public string Value { get; set; }

    /// <inheritdoc/>
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
