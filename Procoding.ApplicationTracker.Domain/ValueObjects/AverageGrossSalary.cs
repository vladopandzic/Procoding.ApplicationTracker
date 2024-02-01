using Procoding.ApplicationTracker.Domain.Common;
using Procoding.ApplicationTracker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.ValueObjects;

public enum Currency
{
    EUR,
    USD,
    None
}

public sealed class AverageGrossSalary : ValueObject
{
    public static AverageGrossSalary None { get; } = new AverageGrossSalary(0, Currency.None);

    public decimal Value { get; }

    public Currency Currency { get; }

    public AverageGrossSalary(decimal value, Currency currency)
    {
        Value = value;
        Currency = currency;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return new object[] { Value, Currency };
    }
}
