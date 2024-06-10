using FluentValidation;
using Procoding.ApplicationTracker.Application.Core.Errors;
using Procoding.ApplicationTracker.Application.Core.Extensions;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Response.JobApplications;

namespace Procoding.ApplicationTracker.Application.JobApplications.Commands.UpdateJobApplication;

public sealed class UpdateJobApplicationCommandValidator : AbstractValidator<UpdateJobApplicationCommand>
{
    public UpdateJobApplicationCommandValidator()
    {
        RuleFor(x => x.CandidateId).NotEmpty().WithError(ValidationErrors.JobApplications.CandidateIsRequired);

        RuleFor(x => x.CompanyId).NotEmpty().WithError(ValidationErrors.JobApplications.CompanyIsRequired);

        RuleFor(x => x.JobApplicationSourceId).NotEmpty().WithError(ValidationErrors.JobApplications.JobApplicationSourceIsRequired);

        RuleFor(x => x.JobAdLink).NotEmpty().WithError(ValidationErrors.JobApplications.JobAdLinkIsRequired);

        RuleFor(x => x.JobAdLink).ValidUrl().WithError(ValidationErrors.JobApplications.JobAdLinkIsNotValidLink);

        RuleFor(x => x.JobType).NotEmpty();

        RuleFor(x => x.JobType).Must(x => JobType.GetAll().Contains(new JobType(x)));

        RuleFor(x => x.WorkLocationType).Must(x => WorkLocationType.GetAll().Contains(new WorkLocationType(x)));

        RuleFor(x => x.JobPositionTitle).NotEmpty();

    }
}
