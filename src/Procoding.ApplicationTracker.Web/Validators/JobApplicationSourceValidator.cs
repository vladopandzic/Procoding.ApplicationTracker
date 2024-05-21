using FluentValidation;
using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.Web.Validators;

public class JobApplicationSourceValidator : FluentValueValidator<JobApplicationSourceDTO>
{
    public JobApplicationSourceValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
