using FluentValidation;
using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.Web.Validators;

public class CandidateValidator : FluentValueValidator<CandidateDTO>
{
    public CandidateValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Email).NotEmpty().EmailAddress();

        RuleFor(x => x.Surname).NotEmpty();

    }
}
