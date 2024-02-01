using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Domain.Entities;

public sealed class JobApplicationSource
{
    public JobApplicationSource(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;
    }

    public string Name { get; }
}
