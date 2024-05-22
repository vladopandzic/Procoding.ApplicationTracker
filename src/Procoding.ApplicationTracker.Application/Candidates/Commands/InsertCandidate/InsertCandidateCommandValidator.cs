using FluentValidation;
using Procoding.ApplicationTracker.Application.Core.Errors;
using Procoding.ApplicationTracker.Application.Core.Extensions;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;

namespace Procoding.ApplicationTracker.Application.Candidates.Commands.InsertCandidate;

public sealed class InsertCandidateCommandValidator : AbstractValidator<InsertCandidateCommand>
{
    public InsertCandidateCommandValidator(ICandidateRepository candidateRepository)
    {
        RuleFor(x => x.Name).NotEmpty().WithError(ValidationErrors.Candidates.NameIsRequried)
                            .MaximumLength(Candidate.MaxLengthForName)
                            .MinimumLength(Candidate.MinLengthForName);

        RuleFor(x => x.Surname).NotEmpty().WithError(ValidationErrors.Candidates.SurnameIsRequired)
                               .MaximumLength(Candidate.MaxLengthForSurname)
                               .MinimumLength(Candidate.MinLengthForSurname);

        RuleFor(x => x.Email).NotEmpty().WithError(ValidationErrors.Candidates.EmailIsRequired);

        RuleFor(x => x.Email).CustomAsync(async (email, validationContext, cancellationToken) =>
        {
            if (await candidateRepository.ExistsAsync(email, Guid.Empty, cancellationToken))
            {
                validationContext.AddFailure(ValidationErrors.Candidates.EmailAlreadyExists.Message);
            }
        });
    }
}
