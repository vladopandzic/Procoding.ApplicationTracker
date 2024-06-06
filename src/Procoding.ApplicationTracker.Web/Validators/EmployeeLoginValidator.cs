using FluentValidation;
using Procoding.ApplicationTracker.DTOs.Request.Employees;

namespace Procoding.ApplicationTracker.Web.Validators;

public class EmployeeLoginValidator : FluentValueValidator<EmployeeLoginRequestDTO>
{
    public EmployeeLoginValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();

        RuleFor(x => x.Password).NotEmpty();

    }
}
