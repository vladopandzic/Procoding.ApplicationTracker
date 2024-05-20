using FluentValidation;
using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.Web.Validators;

public class CompanyValidator : FluentValueValidator<CompanyDTO>
{
    public CompanyValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.OfficialWebSiteLink).NotEmpty().ValidUrl();

    }
}
