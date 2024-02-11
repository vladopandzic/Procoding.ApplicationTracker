using Procoding.ApplicationTracker.Domain.Common;

namespace Procoding.ApplicationTracker.Application.Core.Errors;

internal static class ValidationErrors
{
    /// <summary>
    /// Contains the job application source errors.
    /// </summary>
    internal static class JobApplicationSources
    {
        internal static Error NameIsRequried => new Error("JobApplicationSources.NameIsRequried", "Name is required.");

    }
}
