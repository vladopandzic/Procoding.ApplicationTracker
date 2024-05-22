using FluentValidation;
using Procoding.ApplicationTracker.Application.Core.Errors;
using Procoding.ApplicationTracker.Application.Core.Extensions;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;

namespace Procoding.ApplicationTracker.Application.Companies.Commands.InsertCompany;

public sealed class InsertCompanyCommandValidator : AbstractValidator<InsertCompanyCommand>
{
    public InsertCompanyCommandValidator(ICompanyRepository companyRepository)
    {
        RuleFor(x => x.Name).NotEmpty().WithError(ValidationErrors.Companies.NameIsRequried);

        RuleFor(x => x.OfficialWebSiteLink).NotEmpty().WithError(ValidationErrors.Companies.OfficialWebSiteLinkIsRequried);

        RuleFor(x => x.Name).CustomAsync(async (name, validationContext, cancellationToken) =>
        {
            if (await companyRepository.ExistsAsync(name, Guid.Empty, cancellationToken))
            {
                validationContext.AddFailure(ValidationErrors.Companies.NameAlreadyExists.Message);
            }
        });
    }
}
