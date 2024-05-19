namespace Procoding.ApplicationTracker.DTOs.Request.Candidates;

public record UpdateCandidateRequestDTO(Guid Id, string Name, string Surname, string Email);