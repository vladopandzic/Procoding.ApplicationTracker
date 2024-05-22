using FluentValidation;
using Procoding.ApplicationTracker.Application.Core.Errors;
using Procoding.ApplicationTracker.Application.Core.Extensions;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.InsertJobApplicationSource;

public sealed class AddJobApplicationSourceCommandValidator : AbstractValidator<AddJobApplicationSourceCommand>
{
    public AddJobApplicationSourceCommandValidator(IJobApplicationSourceRepository jobApplicationSourceRepository)
    {
        RuleFor(x => x.Name).NotEmpty().WithError(ValidationErrors.JobApplicationSources.NameIsRequried)
                            .MinimumLength(JobApplicationSource.MinLengthForName)
                            .MaximumLength(JobApplicationSource.MaxLengthForName);

        RuleFor(x => x.Name).CustomAsync(async (name, validationContext, cancellationToken) =>
        {
            if (await jobApplicationSourceRepository.ExistsAsync(name, Guid.Empty, cancellationToken))
            {
                validationContext.AddFailure(ValidationErrors.JobApplicationSources.NameAlreadyExists.Message);
            }
        });
    }
}
