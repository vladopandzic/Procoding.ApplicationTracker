using Procoding.ApplicationTracker.Domain.Common;

namespace Procoding.ApplicationTracker.Domain.ValueObjects;

public sealed class CompanyName : ValueObject
{
    public string Value { get; set; }

    public CompanyName(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        Value = name;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(CompanyName companyName) => companyName.Value;

    public static implicit operator CompanyName(string name) => new CompanyName(name);
}
