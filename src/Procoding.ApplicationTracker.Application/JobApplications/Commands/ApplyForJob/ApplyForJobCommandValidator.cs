using FluentValidation;
using Procoding.ApplicationTracker.Application.Core.Errors;
using Procoding.ApplicationTracker.Application.Core.Extensions;

namespace Procoding.ApplicationTracker.Application.JobApplications.Commands.ApplyForJob;

public sealed class ApplyForJobCommandValidator : AbstractValidator<ApplyForJobCommand>
{
    public ApplyForJobCommandValidator()
    {

        RuleFor(x => x.CandidateId).NotEmpty().WithError(ValidationErrors.JobApplications.CandidateIsRequired);

        RuleFor(x => x.CompanyId).NotEmpty().WithError(ValidationErrors.JobApplications.CompanyIsRequired);

        RuleFor(x => x.JobApplicationSourceId).NotEmpty().WithError(ValidationErrors.JobApplications.JobApplicationSourceIsRequired);

    }
}
