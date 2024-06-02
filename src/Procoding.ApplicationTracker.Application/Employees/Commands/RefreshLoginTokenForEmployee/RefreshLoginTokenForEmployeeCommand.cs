using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Application.Employees.Commands.RefreshLoginTokenForEmployee;

public class RefreshLoginTokenForEmployeeCommand : ICommand<Result<EmployeeLoginResponseDTO>>
{
    public RefreshLoginTokenForEmployeeCommand(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}
