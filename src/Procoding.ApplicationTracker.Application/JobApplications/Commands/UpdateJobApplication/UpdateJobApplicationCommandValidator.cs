using FluentValidation;
using Procoding.ApplicationTracker.Application.Core.Errors;
using Procoding.ApplicationTracker.Application.Core.Extensions;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;

namespace Procoding.ApplicationTracker.Application.JobApplications.Commands.UpdateJobApplication;

internal sealed class UpdateJobApplicationCommandValidator : AbstractValidator<UpdateJobApplicationCommand>
{
    public UpdateJobApplicationCommandValidator()
    {
        RuleFor(x => x.CandidateId).NotEmpty().WithError(ValidationErrors.JobApplications.CandidateIsRequired);

        RuleFor(x => x.CompanyId).NotEmpty().WithError(ValidationErrors.JobApplications.CompanyIsRequired);

        RuleFor(x => x.JobApplicationSourceId).NotEmpty().WithError(ValidationErrors.JobApplications.JobApplicationSourceIsRequired);

    }
}
