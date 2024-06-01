using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Application.Employees.Commands.UpdateEmployee;

public sealed class UpdateEmployeeCommand : ICommand<Result<EmployeeUpdatedResponseDTO>>
{
    public UpdateEmployeeCommand(Guid id, string name, string surname, string email)
    {
        Id = id;
        Name = name;
        Surname = surname;
        Email = email;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }
}