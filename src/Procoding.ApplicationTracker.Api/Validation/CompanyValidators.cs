using FluentValidation;
using Procoding.ApplicationTracker.DTOs.Request.Candidates;
using Procoding.ApplicationTracker.DTOs.Request.Companies;

namespace Procoding.ApplicationTracker.Api.Validation;

public class CompanyInsertRequestDTOValidator : AbstractValidator<CompanyInsertRequestDTO>
{
    public CompanyInsertRequestDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.OfficialWebSiteLink).NotEmpty().ValidUrl();

    }
}
public class CompanyUpdateRequestDTOValidator : AbstractValidator<CompanyUpdateRequestDTO>
{
    public CompanyUpdateRequestDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.OfficialWebSiteLink).NotEmpty().ValidUrl();
    }
}
