using FluentValidation;
using Procoding.ApplicationTracker.Domain.ValueObjects;
using Procoding.ApplicationTracker.DTOs.Request.Employees;
using Procoding.ApplicationTracker.DTOs.Request.JobApplications;
using System.Linq;

namespace Procoding.ApplicationTracker.Api.Validation;

public class JobApplicationInsertRequestDTOValidator : AbstractValidator<JobApplicationInsertRequestDTO>
{
    public JobApplicationInsertRequestDTOValidator()
    {

        RuleFor(x => x.CompanyId).NotEmpty();

        RuleFor(x => x.JobApplicationSourceId).NotEmpty();

        RuleFor(x => x.JobAdLink).NotEmpty();

        RuleFor(x => x.JobAdLink).ValidUrl();

        RuleFor(x => x.JobType).NotEmpty();

        RuleFor(x => x.JobType.Value).Must(x => JobType.GetAll().Contains(new JobType(x)));

        RuleFor(x => x.WorkLocationType.Value).Must(x => WorkLocationType.GetAll().Contains(new WorkLocationType(x)));

        RuleFor(x => x.JobPositionTitle).NotEmpty();
    }
}
public class JobApplicationUpdateRequestDTOValidator : AbstractValidator<JobApplicationUpdateRequestDTO>
{
    public JobApplicationUpdateRequestDTOValidator()
    {
        RuleFor(x => x.CompanyId).NotEmpty();

        RuleFor(x => x.JobApplicationSourceId).NotEmpty();

        RuleFor(x => x.JobAdLink).NotEmpty();

        RuleFor(x => x.JobAdLink).ValidUrl();

        RuleFor(x => x.JobType).NotEmpty();

        RuleFor(x => x.JobType.Value).Must(x => JobType.GetAll().Contains(new JobType(x)));

        RuleFor(x => x.WorkLocationType.Value).Must(x => WorkLocationType.GetAll().Contains(new WorkLocationType(x)));

        RuleFor(x => x.JobPositionTitle).NotEmpty();
    }
}
