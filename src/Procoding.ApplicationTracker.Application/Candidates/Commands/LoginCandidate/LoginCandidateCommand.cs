using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Application.Candidates.Commands.LoginCandidate;

public class LoginCandidateCommand : ICommand<Result<CandidateLoginResponseDTO>>
{
    public LoginCandidateCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }

    public string Password { get; set; }
}
