using FluentValidation;
using Procoding.ApplicationTracker.Application.Core.Errors;
using Procoding.ApplicationTracker.Application.Core.Extensions;
using Procoding.ApplicationTracker.Domain.Repositories;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.UpdateJobApplicationSource;

public class UpdateJobApplicationSourceCommandrValidator : AbstractValidator<UpdateJobApplicationSourceCommand>
{
    public UpdateJobApplicationSourceCommandrValidator(IJobApplicationSourceRepository jobApplicationSourceRepository)
    {
        RuleFor(x => x.Name).NotEmpty().WithError(ValidationErrors.JobApplicationSources.NameIsRequried);

        RuleFor(x => x.Name).CustomAsync(async (name, validationContext, cancellationToken) =>
        {
            if (await jobApplicationSourceRepository.ExistsAsync(name, validationContext.InstanceToValidate.Id, cancellationToken))
            {
                validationContext.AddFailure(ValidationErrors.JobApplicationSources.NameAlreadyExists.Message);
            }
        });
    }
}
