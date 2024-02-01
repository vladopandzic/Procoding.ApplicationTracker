using Procoding.ApplicationTracker.Domain.Common;
using Procoding.ApplicationTracker.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.ValueObjects;

public sealed class Link : ValueObject
{
    public string Value { get; set; }

    public Link(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        if(!Uri.TryCreate(value, UriKind.Absolute, out var uri) || !uri.IsWellFormedOriginalString())
        {
            throw new InvalidUrlException("Supplied value is not valid url");
        }
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
