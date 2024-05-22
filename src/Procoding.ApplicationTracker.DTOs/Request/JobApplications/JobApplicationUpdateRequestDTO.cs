namespace Procoding.ApplicationTracker.DTOs.Request.JobApplications;

/// <summary>
/// Request for inserting job application
/// </summary>
/// <param name="Id"></param>
/// <param name="CandidateId"></param>
/// <param name="JobApplicationSourceId"></param>
/// <param name="CompanyId"></param>
public record JobApplicationUpdateRequestDTO(Guid Id, Guid CandidateId, Guid JobApplicationSourceId, Guid CompanyId);