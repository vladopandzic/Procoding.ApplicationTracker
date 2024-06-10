using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Application.Employees.Commands.UpdateEmployee;

public sealed class UpdateEmployeeCommand : ICommand<Result<EmployeeUpdatedResponseDTO>>
{
    public UpdateEmployeeCommand(Guid id, string name, string surname, string email, string password, bool updatePassword)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
        UpdatePassword = updatePassword;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public bool UpdatePassword { get; set; }
}