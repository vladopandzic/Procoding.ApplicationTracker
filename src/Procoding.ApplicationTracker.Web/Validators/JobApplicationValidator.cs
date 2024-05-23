using FluentValidation;
using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.Web.Validators;

public class JobApplicationValidator : Validators.FluentValueValidator<JobApplicationDTO>
{
    public JobApplicationValidator()
    {
        RuleFor(x => x.Candidate).NotEmpty();

        RuleFor(x => x.Company).NotEmpty();

        RuleFor(x => x.ApplicationSource).NotEmpty();
    }
}
