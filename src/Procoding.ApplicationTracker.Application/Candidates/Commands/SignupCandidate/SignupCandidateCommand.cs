using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Application.Candidates.Commands.SignupCandidate;

public class SignupCandidateCommand : ICommand<Result<CandidateSignupResponseDTO>>
{
    public SignupCandidateCommand(string email, string password, string name, string surname)
    {
        Email = email;
        Password = password;
        Name = name;
        Surname = surname;
    }

    public string Name { get; set; }

    public string Surname { get; set; }


    public string Email { get; set; }

    public string Password { get; set; }
}
