using FluentValidation;
using Procoding.ApplicationTracker.Application.Core.Errors;
using Procoding.ApplicationTracker.Application.Core.Extensions;
using Procoding.ApplicationTracker.Domain.Repositories;

namespace Procoding.ApplicationTracker.Application.Candidates.Commands.UpdateCandidate;

public sealed class UpdateCandidateCommandValidator : AbstractValidator<UpdateCandidateCommand>
{
    public UpdateCandidateCommandValidator(ICandidateRepository candidateRepository)
    {
        RuleFor(x => x.Name).NotEmpty().WithError(ValidationErrors.Candidates.NameIsRequried);

        RuleFor(x => x.Surname).NotEmpty().WithError(ValidationErrors.Candidates.SurnameIsRequired);

        RuleFor(x => x.Email).NotEmpty().WithError(ValidationErrors.Candidates.EmailIsRequired);

        RuleFor(x => x.Email).CustomAsync(async (email, validationContext, cancellationToken) =>
        {
            if (await candidateRepository.ExistsAsync(email, validationContext.InstanceToValidate.Id, cancellationToken))
            {
                validationContext.AddFailure(ValidationErrors.Candidates.EmailAlreadyExists.Message);
            }
        });
    }
}
