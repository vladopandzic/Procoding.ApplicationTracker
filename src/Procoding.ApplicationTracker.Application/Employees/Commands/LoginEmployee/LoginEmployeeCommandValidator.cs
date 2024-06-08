using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Procoding.ApplicationTracker.Domain.Entities;
namespace Procoding.ApplicationTracker.Application.Employees.Commands.LoginEmployee;

public sealed class LoginEmployeeCommandValidator : AbstractValidator<LoginEmployeeCommand>
{
    public LoginEmployeeCommandValidator(UserManager<Employee> userManager)
    {
        //    RuleFor(x => x.Password).CustomAsync(async (email, validationContext, cancellationToken) =>
        //    {
        //        Employee? employee = await userManager.FindByEmailAsync(validationContext.InstanceToValidate.Password);

        //        if (employee is null ||
        //            employee.DeletedOnUtc is not null ||
        //            await userManager.CheckPasswordAsync(employee, validationContext.InstanceToValidate.Password) == false)
        //        {
        //            validationContext.AddFailure(ValidationErrors.Employees.EmailAlreadyExists.Message);

        //            return new Result<EmployeeLoginResponseDTO>(new Unauthorized401Exception("Invalid username or password"));
        //        }
        //    });
    }
}
