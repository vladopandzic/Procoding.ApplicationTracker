﻿using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Employees;

namespace Procoding.ApplicationTracker.Application.Employees.Commands.InsertEmployee;

public sealed class InsertEmployeeCommand : ICommand<Result<EmployeeInsertedResponseDTO>>
{
    public InsertEmployeeCommand(string name, string surname, string email, string password)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
    }

    public string Name { get; }

    public string Surname { get; }

    public string Email { get; }

    public string Password { get; set; }
}
