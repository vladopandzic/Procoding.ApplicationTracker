using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Application.Candidates.Commands.InsertCandidate;

public sealed class InsertCandidateCommand : ICommand<Result<CandidateInsertedResponseDTO>>
{
    public InsertCandidateCommand(string name, string surname, string email, string password)
    {
        Name = name;
        Surname = surname;
        Email = email;
        Password = password;
    }

    public string Name { get; }

    public string Surname { get; }

    public string Email { get; }

    public string Password { get; }
}