using Procoding.ApplicationTracker.Domain.Common;

namespace Procoding.ApplicationTracker.Application.Core.Errors;

internal static class ValidationErrors
{
    /// <summary>
    /// Contains the job application source errors.
    /// </summary>
    internal static class JobApplicationSources
    {
        internal static Error NameIsRequried => new Error("JobApplicationSources.NameIsRequired", "Name is required.");

        internal static Error NameAlreadyExists => new Error("JobApplicationSources.NameAlreadyExists", "Job application source with that name already exist.");

    }

    /// <summary>
    /// Contains the company errors.
    /// </summary>
    internal static class Companies
    {
        internal static Error NameIsRequried => new Error("Companies.NameIsRequired", "Name is required.");

        internal static Error OfficialWebSiteLinkIsRequried => new Error("Companies.OfficialWebSiteLinkIsRequried", "Official web site link is required.");

        internal static Error NameAlreadyExists => new Error("Companies.NameAlreadyExists", "Company with that name already exist.");

    }

    /// <summary>
    /// Contains the candidates errors.
    /// </summary>
    internal static class Candidates
    {
        internal static Error NameIsRequried => new Error("Candidates.NameIsRequired", "Name is required.");

        internal static Error SurnameIsRequired => new Error("Candidates.SurnameIsRequired", "Surname is required.");

        internal static Error EmailIsRequired => new Error("Candidates.EmailIsRequired", "Email is required.");

        internal static Error EmailAlreadyExists => new Error("Candidates.EmailAlreadyExist", "Candidate with that email already exist.");

    }

    /// <summary>
    /// Contains the company errors.
    /// </summary>
    internal static class Employees
    {
        internal static Error NameIsRequried => new Error("Employees.NameIsRequired", "Name is required.");

        internal static Error SurnameIsRequired => new Error("Employees.SurnameIsRequired", "Surname is required.");

        internal static Error EmailIsRequired => new Error("Employees.EmailIsRequired", "Email is required.");

        internal static Error EmailAlreadyExists => new Error("Employees.EmailAlreadyExist", "Employee with that email already exist.");

    }

    /// <summary>
    /// Contains the job application errors.
    /// </summary>
    internal static class JobApplications
    {
        internal static Error CandidateIsRequired => new Error("JobApplications.CandidateIsRequired", "Candidate is required.");

        internal static Error JobApplicationSourceIsRequired => new Error("JobApplications.JobApplicationSourceIsRequired", "JobApplicationSource is required.");

        internal static Error JobAdLinkIsRequired => new Error("JobApplications.JobAdLinkIsRequired", "Job Ad Link is required.");

        internal static Error JobAdLinkIsNotValidLink => new Error("JobApplications.JobAdLinkIsNotValidLink", "Job ad link is not a valid link.");


        internal static Error CompanyIsRequired => new Error("JobApplications.CompanyIsRequired", "Company is required.");


    }
}
