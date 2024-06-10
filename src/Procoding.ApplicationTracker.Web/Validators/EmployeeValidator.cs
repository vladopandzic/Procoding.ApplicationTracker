using FluentValidation;
using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.Web.Validators;

public class EmployeeValidator : FluentValueValidator<EmployeeEditDTO>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Email).NotEmpty().EmailAddress();

        RuleFor(x => x.Surname).NotEmpty();

        RuleFor(x => x.Password)
           .NotEmpty()
           .When(x => x.UpdatePassword)
           .WithMessage("Password must not be empty or null when updating the password.");
    }

}
