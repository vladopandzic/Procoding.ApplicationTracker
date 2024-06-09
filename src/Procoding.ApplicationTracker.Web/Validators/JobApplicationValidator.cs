using FluentValidation;
using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.Web.Validators;

public class JobApplicationValidator : Validators.FluentValueValidator<JobApplicationDTO>
{
    public JobApplicationValidator()
    {
        RuleFor(x => x.Candidate).NotEmpty();

        RuleFor(x => x.Company).NotEmpty();

        RuleFor(x => x.ApplicationSource).NotEmpty();


        RuleFor(x => x.JobPositionTitle).NotEmpty();


        RuleFor(x => x.JobAdLink).NotEmpty();

        RuleFor(x => x.JobAdLink).Custom((jobAdLink, context) =>
        {
            if (jobAdLink is not null)
            {
                if (string.IsNullOrEmpty(jobAdLink))
                {
                    context.AddFailure("Job ad link must not be empty");
                }
            }
        });

        RuleFor(x => x.WorkLocation).NotEmpty();

        RuleFor(x => x.WorkLocation).Custom((workLocation, context) =>
        {
            if (workLocation is not null)
            {
                if (string.IsNullOrEmpty(workLocation.Value))
                {
                    context.AddFailure("Work location must not be empty");
                }
            }
        });

        RuleFor(x => x.JobType).NotEmpty();

        RuleFor(x => x.JobType).Custom((jobType, context) =>
        {
            if (jobType is not null)
            {
                if (string.IsNullOrEmpty(jobType.Value))
                {
                    context.AddFailure("Job type must not be empty");
                }
            }
        });
    }
}
