using FluentValidation;
using Procoding.ApplicationTracker.Application.Core.Errors;
using Procoding.ApplicationTracker.Application.Core.Extensions;
using Procoding.ApplicationTracker.Domain.Repositories;

namespace Procoding.ApplicationTracker.Application.Companies.Commands.UpdateCompany;

public sealed class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator(ICompanyRepository companyRepository)
    {
        RuleFor(x => x.CompanyName).NotEmpty().WithError(ValidationErrors.Companies.NameIsRequried);

        RuleFor(x => x.OfficialWebsiteLink).NotEmpty().WithError(ValidationErrors.Companies.OfficialWebSiteLinkIsRequried);

        RuleFor(x => x.CompanyName).CustomAsync(async (name, validationContext, cancellationToken) =>
        {
            if (await companyRepository.ExistsAsync(name, validationContext.InstanceToValidate.Id, cancellationToken))
            {
                validationContext.AddFailure(ValidationErrors.Companies.NameAlreadyExists.Message);
            }
        });
    }
}
