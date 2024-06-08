using Procoding.ApplicationTracker.DTOs.Model;

namespace Procoding.ApplicationTracker.DTOs.Request.JobApplications;

/// <summary>
/// Request for inserting job application
/// </summary>
/// <param name="CandidateId"></param>
/// <param name="JobApplicationSourceId"></param>
/// <param name="CompanyId"></param>
public record JobApplicationInsertRequestDTO(Guid CandidateId,
                                             Guid JobApplicationSourceId,
                                             Guid CompanyId,
                                             string JobPositionTitle,
                                             string JobAdLink,
                                             JobTypeDTO JobType,
                                             WorkLocationTypeDTO WorkLocationType,
                                             string? Description);