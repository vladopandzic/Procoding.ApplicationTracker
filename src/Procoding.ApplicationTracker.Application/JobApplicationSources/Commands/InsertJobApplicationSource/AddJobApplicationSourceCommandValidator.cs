using FluentValidation;
using Procoding.ApplicationTracker.Application.Core.Errors;
using Procoding.ApplicationTracker.Application.Core.Extensions;

namespace Procoding.ApplicationTracker.Application.JobApplicationSources.Commands.InsertJobApplicationSource
{

    public sealed class AddJobApplicationSourceCommandValidator : AbstractValidator<AddJobApplicationSourceCommand>
    {
        public AddJobApplicationSourceCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithError(ValidationErrors.JobApplicationSources.NameIsRequried);

        }
    }
}
