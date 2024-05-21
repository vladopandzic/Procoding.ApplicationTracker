using FluentValidation;
using Procoding.ApplicationTracker.DTOs.Request.JobApplicationSources;

namespace Procoding.ApplicationTracker.Api.Validation;

public class JobApplicationSourceInsertRequestDTOValidator : AbstractValidator<JobApplicationSourceInsertRequestDTO>
{
    public JobApplicationSourceInsertRequestDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

    }
}
public class JobApplicationSourceUpdateRequestDTOValidator : AbstractValidator<JobApplicationSourceUpdateRequestDTO>
{
    public JobApplicationSourceUpdateRequestDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

    }
}
