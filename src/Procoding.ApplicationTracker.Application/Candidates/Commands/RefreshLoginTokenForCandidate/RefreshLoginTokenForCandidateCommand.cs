using LanguageExt.Common;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;

namespace Procoding.ApplicationTracker.Application.Candidates.Commands.RefreshLoginTokenForCandidate;

public class RefreshLoginTokenForCandidateCommand : ICommand<Result<CandidateLoginResponseDTO>>
{
    public RefreshLoginTokenForCandidateCommand(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}
