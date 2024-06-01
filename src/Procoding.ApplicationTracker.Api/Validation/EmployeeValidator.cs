using FluentValidation;
using Procoding.ApplicationTracker.DTOs.Request.Employees;

namespace Procoding.ApplicationTracker.Api.Validation;

public class EmployeeInsertRequestDTOValidator : AbstractValidator<EmployeeInsertRequestDTO>
{
    public EmployeeInsertRequestDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Surname).NotEmpty();

        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
public class EmployeeUpdateRequestDTOValidator : AbstractValidator<EmployeeUpdateRequestDTO>
{
    public EmployeeUpdateRequestDTOValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Surname).NotEmpty();

        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
