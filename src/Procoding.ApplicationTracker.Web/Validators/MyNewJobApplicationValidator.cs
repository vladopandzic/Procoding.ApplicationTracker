using FluentValidation;
using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.Web.Validators;

public class MyNewJobApplicationValidator : Validators.FluentValueValidator<JobApplicationDTO>
{
    public MyNewJobApplicationValidator()
    {

        RuleFor(x => x.Company).NotEmpty();

        RuleFor(x => x.Company).Custom((company, context) =>
        {
            if (company is not null)
            {
                if (string.IsNullOrEmpty(company.CompanyName) || company.Id == Guid.Empty)
                {
                    context.AddFailure("Company must be choosen");
                }
            }
        });

        RuleFor(x => x.ApplicationSource).NotEmpty();

        RuleFor(x => x.ApplicationSource).Custom((applicationSource, context) =>
        {
            if (applicationSource is not null)
            {
                if (string.IsNullOrEmpty(applicationSource.Name) || applicationSource.Id == Guid.Empty)
                {
                    context.AddFailure("Job applicationSource must be choosen");
                }
            }
        });
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
