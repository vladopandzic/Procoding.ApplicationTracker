namespace Procoding.ApplicationTracker.DTOs.Response.Candidates;

public class CandidateSignupResponseDTO
{
    public CandidateSignupResponseDTO(bool succedeed)
    {
        Succedeed = succedeed;
    }

    public bool Succedeed { get; set; }
}
