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


    }
}
