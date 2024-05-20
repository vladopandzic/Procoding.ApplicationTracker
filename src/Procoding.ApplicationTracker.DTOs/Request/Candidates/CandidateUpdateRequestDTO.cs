namespace Procoding.ApplicationTracker.DTOs.Request.Candidates;

public record CandidateUpdateRequestDTO(Guid Id, string Name, string Surname, string Email);